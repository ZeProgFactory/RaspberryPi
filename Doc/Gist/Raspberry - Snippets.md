# Raspberry - Snippets

## Session autostart
```
sudo nano /etc/xdg/lxsession/LXDE-pi/autostart
```

## Apache
Install apache
```
sudo apt install apache2 -y
```

Get the current state off the apache service 
```
sudo service apache2 status
```

default html root folder
```
/var/www/html
```


## Maintenance clean any remaining stuff
```
sudo apt-get --yes autoremove
sudo apt-get --yes autoclean
sudo apt-get --yes clean
```

## Install SAMBA

```
sudo apt-get update
sudo apt-get upgrade
```

Now our Raspbian OS should be up to date. Now we can install the Samba package by running the following command:

```
sudo apt-get install samba samba-common-bin
```
   
Configuring Samba

```
sudo nano /etc/samba/smb.conf
```

Within this config file, scroll write down to the bottom and add the following text:

```
[Data]
path= /home/pi
writable = yes
public=no
create mask=0777
directory mask=0777
```

Set password for Samba
```
sudo smbpasswd -a root
```

Restart Samba service in order to take the chagements into account:
```
sudo service smbd restart
```

  
## Setup .NET Core 3.0 Runtime and SDK on Raspberry Pi 4  
https://edi.wang/post/2019/9/29/setup-net-core-30-runtime-and-sdk-on-raspberry-pi-4   
https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.100-linux-arm32-binaries  

## Raspberry Pi Using Conky    
https://www.youtube.com/watch?v=ElwH2BOov-Q
https://www.lifewire.com/beginners-guide-to-conky-4043352#:~:text=%20A%20Beginner%27s%20Guide%20to%20Conky%20%201,is%20located%20in%20%2Fetc%2Fconky%2Fconky.conf.%20You%20should...%20More%20


## Raspberry Pi Using BGINFO4X   
Install the prerequisites:
```
sudo apt-get install imagemagick
sudo apt-get install root-tail 
``` 
(Download)[http://sourceforge.net/projects/bginfo4x/files/latest/download] the latest version of BGINFO4X.  
Copy the downloaded file to a'/tmp' folder.  
Install BGINFO4X  
 ```
cd /tmp
tar xvfz "Last version of BGINFO4X.tar.gz"
sudo mv BGINFO4X /usr/local/bin (Install it in a System-wide Administration Folder)
cd /usr/local/bin/BGINFO4X
``` 
Start the program
``` 
./BGINFO4X.sh 
``` 
Autostart the program
 http://wiki.lxde.org/en/Autostart or http://www.raspberrypi.org/phpBB3/viewtopic.php?f=27&t=11256   
 
 
## Rotate Touch Screen
```
sudo nano /boot/config.txt
```

Add to the top of the file:
```
lcd_rotate=2
```

| 0 | no change |   
| 1 | rotates 90 degrees | 
| 2 | rotates 180 degrees | 
| 3 | rotates 270 degrees |
    
The changes will be taken into account during the next restart.        
```
sudo reboot
```    
    

Set NTP time auto
share samba / Windows

https://www.amazon.fr/Waveshare-WM8960-Audio-Raspberry-Interface/dp/B07KN8424G  
https://www.waveshare.com/wm8960-audio-hat.htm#:~:text=WM8960%20Hi-Fi%20Sound%20Card%20HAT%20for%20Raspberry%20Pi%2C,3.3V%20Earphone%20driver%3A%2040mW%20%2816%CE%A9%403.3V%29%20More%20items...%20  
https://www.waveshare.com/w/upload/5/54/WM8960_Audio_HAT_User_Manual_EN.pdf  


? Edge - https://www.microsoftedgeinsider.com/en-us/download/?platform=linux-deb    
? github  
? prog .Net / .Net IOT  
? file trans  
? touch screen  
