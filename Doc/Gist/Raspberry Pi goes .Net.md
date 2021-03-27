# Raspberry Pi goes .Net
## Install VNC on the Raspberry Pi
based on https://raspberrytips.com/remote-desktop-raspberry-pi/#Remote_desktop_with_SSH_and_X11_forwarding

We will install the RealVNC server on the Raspberry Pi, which is available in the repositories
Follow the steps below to install it:

Update your repository :
```
sudo apt-get update
```

Install RealVNC server :
```
sudo apt-get install realvnc-vnc-server
```

Enable VNC Server :
Start raspbian configuration
```
sudo raspi-config
```

Choose ```3-Interfacing``` options 
Select ```VNC``` 
Answer Yes  
Select Finish to quit (or ESC).  
Now that we finished installing VNC, let’s see how to connect

#### From Windows
From Windows it is the same thing, you can [download](https://www.realvnc.com/fr/connect/download/viewer/windows/) and install RealVNC from their website.

Then launch the software via the start menu, type the IP of the Raspberry Pi and here you are connected to the remote desktop

The requested logins are the usual users of the system (for example pi/raspberry if you have not changed the password)

#### From Mac OS
RealVNC is also available for Mac OS  so just get and install it and then follow the same steps as below 🙂  

  

## Install .NET Core On Raspberry Pi
based on https://blog.technitium.com/2019/01/quick-and-easy-guide-to-install-net.html
.NET Core is a cross-platform runtime available for x64 and ARM processors that can be used to run ASP.NET Core web applications and standalone .NET Core console applications on Windows, Linux and macOS.

Installing .NET Core is straight forward for most Desktop platforms with clear instructions available on the download website. However, many would find it trickier to install it on something like Raspberry Pi which uses ARM based processor. So, here is a quick and easy guide to install .NET Core 2.2 on Raspberry Pi 3 Model B+ with the latest Raspbian that is based on Debian 9 (Stretch).

Connect to your Raspberry Pi using SSH or VNC and get started!

### Installing Dependencies
First you need to install a few dependencies required by the .NET Core runtime:

```
sudo apt-get -y update
sudo apt-get -y install curl libunwind8 gettext apt-transport-https
```

### Installing .NET Core
Go to the .NET Core download page and download the Linux ARM32 runtime. Or you could just copy the download URL from there to use with wget like I did and follow these steps:

```
wget https://download.visualstudio.microsoft.com/download/pr/1c5366e8-9b74-4017-96ae-47fc08832c22/504aed87590bd99c49d053bc6f980b6b/aspnetcore-runtime-5.0.0-linux-arm.tar.gz
sudo mkdir -p /opt/dotnet
sudo tar -zxf aspnetcore-runtime-5.0.0-linux-arm.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/bin
```



```
wget https://download.visualstudio.microsoft.com/download/pr/8f09af48-e88e-4b91-bae1-08a5c9183559/e10eefacab56a4f4c1165d4e26a5f0f9/dotnet-sdk-5.0.200-linux-arm.tar.gz
sudo mkdir -p /opt/dotnet
sudo tar -zxf dotnet-sdk-5.0.200-linux-arm.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/bin
```





Now just enter ```dotnet``` on the command line to confirm.

Its Done!
Now you are ready to run ASP.NET Core or .NET Core console apps on your Raspberry Pi!



### Installing .NET Core WebApplication
#### VisualStudio publish profile
```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
https://go.microsoft.com/fwlink/?LinkID=208121. 
-->
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <DeleteExistingFiles>True</DeleteExistingFiles>
    <ExcludeApp_Data>False</ExcludeApp_Data>
    <LaunchSiteAfterPublish>True</LaunchSiteAfterPublish>
    <LastUsedBuildConfiguration>Release</LastUsedBuildConfiguration>
    <LastUsedPlatform>Any CPU</LastUsedPlatform>
    <PublishProvider>FileSystem</PublishProvider>
    <PublishUrl>bin\Release\net5.0\publish\</PublishUrl>
    <WebPublishMethod>FileSystem</WebPublishMethod>
    <SiteUrlToLaunchAfterPublish />
    <TargetFramework>net5.0</TargetFramework>
    <RuntimeIdentifier>linux-arm</RuntimeIdentifier>
    <ProjectGuid>eaec3827-b35e-4e68-97ca-b8eac4f3b086</ProjectGuid>
    <SelfContained>true</SelfContained>
    <PublishSingleFile>False</PublishSingleFile>
    <PublishTrimmed>True</PublishTrimmed>
  </PropertyGroup>
</Project>
```

#### Actions on the Pi
Copy publish folder ...
```
sudo chmod +x WebApplication
./WebApplication
```


```
./WebApplication --urls "http://*:5080"
```



## Xamarin.Forms
### Install Mono on the Raspberry

```
sudo apt-get install mono-complete
```

Then you need to install the bindings between Mono and GTK:  
```
sudo apt-get install gtk-sharp2
```

The Xamarin.Forms GTK backend makes use of webkit-sharp:
```
sudo apt-get install libwebkitgtk-dev
```

Copy the output of a Xamarin.Forms GTK# Backend application and run:  
```
mono Xamarin.Forms.App.GTK.exe
```

### Install all
```
sudo apt-get install mono-complete
sudo apt-get install gtk-sharp2
sudo apt-get install libwebkitgtk-dev
```

### Uninstall all
```
sudo apt-get remove libwebkitgtk-dev
sudo apt-get remove gtk-sharp2
sudo apt-get remove mono-complete
```

```
sudo nano /etc/mono/config   
```

add following lines to mono config for the necessary file mappings:
```
<dllmap dll="libglib-2.0-0.dll" target="libglib-2.0.so.0" os="linux" />
<dllmap dll="libgtk-win32-2.0-0.dll" target="libgtk-3.so.0" os="linux"/>
<dllmap dll="libgobject-2.0-0.dll" target="libgobject-2.0.so.0" os="linux"/>

<dllmap dll="gtksharpglue-2" target="/usr/lib/cli/gtk-sharp-2.0/libgtksharpglue-2.so" os="!windows"/> 
<dllmap dll="gtksharpglue-2" target="/usr/lib/cli/glib-sharp-2.0/libglibsharpglue-2.so" os="linux"/>

<dllmap dll="glibsharpglue-2" target="/usr/lib/cli/glib-sharp-2.0/libglibsharpglue-2.so" os="!windows"/>   
<dllmap dll="libpango-1.0-0.dll" target="libpango-1.0.so" os="!windows"/> 
<dllmap dll="libgdk-win32-2.0-0.dll" target="libgdk-x11-2.0.so" os="!windows"/> 
<dllmap dll="libatk-1.0-0.dll" target="libatk-1.0.so" os="!windows"/> 
<dllmap dll="pangosharpglue-2" target="/usr/lib/cli/pango-sharp-2.0/libpangosharpglue-2.so" os="!windows"/> 
<dllmap dll="gdksharpglue-2" target="/usr/lib/cli/gdk-sharp-2.0/libgdksharpglue-2.so" os="!windows"/> 
<dllmap dll="libgdk_pixbuf-2.0-0.dll" target="libgdk_pixbuf-2.0.so" os="!windows"/> 
```

See if lib exists
```
find /usr -name *gdksharpglue*
```

