using System;
using System.Diagnostics;

namespace MotivationEmailsService
{
    public class clsLogger
    {
        private string _Source = "MotivationEmailsService";
        public clsLogger()
        {
            if (!EventLog.SourceExists(_Source))
                EventLog.CreateEventSource(_Source, "Application");
        }

        public void Log(string Message, EventLogEntryType EntryType)
        {
            string TimeStamp = $"{DateTime.Now.ToString("HH:mm:ss")}";
            EventLog.WriteEntry(_Source, $"{TimeStamp} {Message}", EntryType);
            if (Environment.UserInteractive)
                Console.WriteLine($"{TimeStamp} {Message}");
        }
    }
}
