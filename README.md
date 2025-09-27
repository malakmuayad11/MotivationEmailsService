# Motivation Emails Service
A Windows service that sends a motivational email to a user. The service can be scheduled using Task Scheduler.
# Build Instructions (Release Mode):
1. Save the source code.
2. Build the solution (ctrl + shift + b) in release mode.
# Deployment Instructions:
### Installation (Using Batch File):
1. Open the solution's folder in File Explorer.
2. Open bin -> release -> copy file path **(service's file path)**.
3. Open the InstallScript file.
4. Open the InstallScript file.
5. Change the binPath directory to the **service's file path**.
6. Reopen the InstallScript with the command prompt in **administrator mode**.
### Start Service:
1. Open the command prompt in administrator mode.
2. Use this command: sc start MotivationEmailsService
### Stop Service:
1. Open the command prompt in administrator mode.
2. Use this command: sc stop MotivationEmailsService
# Uninstallation (Using Batch File):
1. Open the UnistallScript file with the command prompt in administrator mode.
