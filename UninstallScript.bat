@echo off
echo Stopping and Uninstalling Motivation Emails Service...

sc stop MotivationEmailsService
sc delete MotivationEmailsService

echo MotivationEmailsService uninstalled successfully.
pause