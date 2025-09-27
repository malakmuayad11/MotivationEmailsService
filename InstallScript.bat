@echo off
echo Installing Motivation Emails Service...
 
sc create MyService binPath= "C:\Path\To\YourService.exe" start= auto
sc start MyService

echo Service installed and started successfully.
pause