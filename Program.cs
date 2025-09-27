using System;
using System.ServiceProcess;

namespace MotivationEmailsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                Console.WriteLine("Service started in console mode..");
                MotivationEmailsService service = new MotivationEmailsService();
                service.StartInConsole();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new MotivationEmailsService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
