

[Install and use Microsoft Dot NET 9 with the Raspberry Pi](https://www.petecodes.co.uk/install-and-use-microsoft-dot-net-9-with-the-raspberry-pi/)  

```
wget -O - https://raw.githubusercontent.com/pjgpetecodes/dotnet9pi/main/install.sh | sudo bashè
```

```
sudo reboot
```

crontab -e

Add a line starting with @reboot, followed by your script command, like this:
@reboot /home/pat/Desktop/test.sh

@midnight /sbin/shutdown -r now


[RP #03: Creating a .NET Service to run automatically on a Raspberry Pi](https://medium.com/medialesson/rp-03-creating-a-net-service-to-run-automatically-on-a-raspberry-pi-a7554c11e664)  
