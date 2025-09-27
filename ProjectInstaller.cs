using System.ComponentModel;
using System.ServiceProcess;

namespace MotivationEmailsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;
        public ProjectInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            // Service configuration
            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "MotivationEmailsService",
                DisplayName = "Motivation Emails Service",
                StartType = ServiceStartMode.Automatic,
                Description = "A Windows service that sends an email message at the specfied time.",
                ServicesDependedOn = new string[] { "MSSQLSERVER", "EventLog", "RpcSs"} // Dependencies
            };
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
