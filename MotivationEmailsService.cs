using System;
using System.Configuration;
using System.ServiceProcess;
using System.Diagnostics;

namespace MotivationEmailsService
{
    public partial class MotivationEmailsService : ServiceBase
    {
        private readonly string _EmailTo;
        private readonly string _EmailFrom;
        private string _Receiver;
        private string _Sender;
        private readonly clsLogger _Logger;

        public MotivationEmailsService()
        {
            InitializeComponent();
            _EmailTo = ConfigurationManager.AppSettings["EmailTo"];
            _EmailFrom = ConfigurationManager.AppSettings["EmailFrom"];
            _Receiver = ConfigurationManager.AppSettings["Receiver"];
            _Sender = ConfigurationManager.AppSettings["Sender"];
            _Logger = new clsLogger();
            _HandleConfigurations();
        }

        private void _HandleConfigurations()
        {
            if(string.IsNullOrEmpty(_EmailTo) || string.IsNullOrEmpty(_EmailFrom)) 
            {
                _Logger.Log("Emails cannot be empty. Serivce is stopped.", EventLogEntryType.Error);
                this.Stop();
            }

            if(string.IsNullOrEmpty(_Receiver))
            {
                _Receiver = "Unknown";
                _Logger.Log("Receiver's name is empty and replaced with \"Unknown\"", EventLogEntryType.Warning);
            }

            if (string.IsNullOrEmpty(_Sender))
            {
                _Sender = "Unknown";
                _Logger.Log("Sender's name is empty and replaced with \"Unknown\"", EventLogEntryType.Warning);
            }
        }
        protected override void OnStart(string[] args)
        {
            _Logger.Log("Service Started", EventLogEntryType.Information);
            clsMessage.SendMessage(_EmailFrom, _EmailTo, _Receiver, _Sender);
        }

        protected override void OnStop() 
             => _Logger.Log("Service Stopped", EventLogEntryType.Information); 

        public void StartInConsole()
        {
            OnStart(null);
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine();
            OnStop();
        }
    }
}
