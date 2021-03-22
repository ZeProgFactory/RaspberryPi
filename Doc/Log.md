# Install Log

08/02/2021 - Rasbian "full"


## Update the system

```shell-session
sudo apt update
sudo apt upgrade
sudo apt dist-upgrade
sudo reboot
```

## SSH & VNC

## Samba
```shell-session
sudo apt-get install samba samba-common-bin
sudo apt-get install leafpad 
sudo leafpad /etc/samba/smb.conf
```

```
[Data]
Comment = Pi shared folder
Path = /home/pi
Browseable = yes
Writeable = Yes
only guest = no
create mask = 0777
directory mask = 0777
Public = no
```

```shell-session
sudo smbpasswd -a pi
sudo /etc/init.d/samba restart
```