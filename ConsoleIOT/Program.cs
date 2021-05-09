using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.CharacterLcd;
using Iot.Device.CpuTemperature;
using Iot.Device.Pcx857x;

namespace IOT
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         System.Console.WriteLine("Hello World!");

         if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
         {
            Pcf8574tSample.SampleEntryPoint();

            //{
            //// https://github.com/dotnet/iot/blob/main/src/devices/CharacterLcd/README.md
            //// https://github.com/dotnet/iot/commit/3e3306d87fea40cfa5fe649c2ebcb2fbae47344c#diff-deca58a552d86d7e5e896af6751a0bd68aa634a0df31a10f2bcffe9f59a7adda
            //// https://wiki.52pi.com/index.php/PCF8574T_LCD_Driver_Board_SKU:_D-0006
            //}

            // MSP23008

         };


         if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
         {
         };

         if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
         {
         };

         // - - -  - - - 

         CpuTemperature cpuTemperature = new CpuTemperature();
         var i = cpuTemperature.ReadTemperatures();
         if (cpuTemperature.IsAvailable)
         {
            double temperature = cpuTemperature.Temperature.DegreesCelsius;

            if (!double.IsNaN(temperature))
            {
               var temperatures = cpuTemperature.ReadTemperatures();

               foreach (var entry in temperatures)
               {
                  System.Console.WriteLine($"CPU Temperature: {temperature} C");

                  if (!double.IsNaN(entry.Item2.DegreesCelsius))
                  {
                     System.Console.WriteLine($"Temperature from {entry.Item1.ToString()}: {entry.Item2.DegreesCelsius} °C");
                  }
                  else
                  {
                     System.Console.WriteLine("Unable to read Temperature.");
                  };
               };
            };
         }
         else
         {
            System.Console.WriteLine($"CPU temperature is not available");
         };

         // - - -  - - - 
      }
   }
}
