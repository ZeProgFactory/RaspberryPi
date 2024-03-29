﻿using System.Device.I2c;   // Nuget: System.Device.Gpio

namespace ZPF.IOT
{
    public static class IOTHelper
    {
        static int tmp102Address = 0x48;
        //int tmp102Address = 0x91 >> 1;
        static I2cDevice i2c = null;

        public static string LastMessage { get; private set; } = "";

        public static double GetTemp()
        {
            LastMessage = "";

            if (i2c == null)
            {
                // will create an I2C device on the bus 1(the default one) and with the device address 'tmp102Address'

                try
                {
                    i2c = I2cDevice.Create(new I2cConnectionSettings(1, tmp102Address));
                }
                catch(Exception ex)
                {
                    LastMessage = ex.Message;
                    return 0;
                };
            };

            byte[] dump = new byte[2];
            i2c.Read(dump);

            byte MSB = dump[0];
            byte LSB = dump[1];

            double fDigitalTemp;
            short iDigitalTemp;

            // Bit 0 of second byte will always be 0 in 12-bit readings and 1 in 13-bit
            if ((byte)(LSB & 0x01) > 0)  // 13 bit mode
            {
                // Combine bytes to create a signed int
                iDigitalTemp = (short)(((MSB) << 5) | (LSB >> 3));

                // Temperature data can be + or -, if it should be negative,
                // convert 13 bit to 16 bit and use the 2s compliment.
                if (iDigitalTemp > 0xFFF)
                {
                    iDigitalTemp = (short)(iDigitalTemp | 0xE000);
                }
            }
            else  // 12 bit mode
            {
                // Combine bytes to create a signed int 
                iDigitalTemp = (short)(((MSB) << 4) | (LSB >> 4));

                // Temperature data can be + or -, if it should be negative,
                // convert 12 bit to 16 bit and use the 2s compliment.
                if (iDigitalTemp > 0x7FF)
                {
                    iDigitalTemp = (short)(iDigitalTemp | 0xF000);
                }
            }

            // Convert digital reading to analog temperature (1-bit is equal to 0.0625 C)
            fDigitalTemp = iDigitalTemp * 0.0625;

            return fDigitalTemp;
        }
    }
}
