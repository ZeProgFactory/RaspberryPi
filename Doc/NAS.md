
# Update your Raspberry Pi
sudo apt update && sudo apt full upgrade  
sudo apt install rpi-eeprom  
sudo rpi-eeprom-update  
    
    
# Configure the boot order
sudo raspi-config


# Install OpenMediaVault
sudo wget -O - https://github.com/OpenMediaVault-Plugin-Developers/installScript/raw/master/install | sudo bash


## Default credentials to log in for the first time:
Login: admin  
Password: openmediavault  


# Install Midnight Commander
sudo apt-get install mc -y  


# ARGON EON
curl https://download.argon40.com/argoneon.sh | bash
reboot

## ARGON EON Configuration
argon-config
  
  
# TestDisk on Raspberry Pi
sudo apt install testdisk  
sudo testdisk