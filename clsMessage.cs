using MimeKit;
using System;
using System.Configuration;
using MailKit.Net.Smtp;
using System.Diagnostics;
using System.Data;

namespace MotivationEmailsService
{
    public static class clsMessage
    {
        private static DataTable _MotivationalMessages;
        private static clsLogger _Logger;
        private static string _Server;
        private static int? _PortNumber;
        private static string _AuthenticationPassword;

        static clsMessage()
        {
            _Logger = new clsLogger();  
            _MotivationalMessages = clsMessageData.GetAllMessages();
            if (_MotivationalMessages == null)
                _Logger.Log("Motivational messages data table is empty", EventLogEntryType.Error);
            _HandleConfigurations();
        }

        private static void _HandleConfigurations()
        {
            _Server = ConfigurationManager.AppSettings["Server"];
            _PortNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"]);
            _AuthenticationPassword = ConfigurationManager.AppSettings["AuthenticationPassword"];

            if (_Server == null)
            {
                _Server = "smtp.gmail.com";
                _Logger.Log("Email server is not provided and replaced with default server.", EventLogEntryType.Warning);
            }

            if (_PortNumber == null)
            {
                _PortNumber = 587;
                _Logger.Log("Port Number is not provided and replaced with default port.", EventLogEntryType.Warning);
            }

            if (_AuthenticationPassword == null)
                _Logger.Log("Authentication Password is empty, the service may not complete.", EventLogEntryType.Error);
        }

        private static int GetRandomNumber(int From, int To)
        {
            if(_MotivationalMessages == null || _MotivationalMessages.Rows.Count == 0)
            {
                _Logger.Log("Motivational data table cannot be empty.", EventLogEntryType.Error);
                throw new Exception("Motivational data table cannot be empty.");
            }
            Random random = new Random();
            return random.Next(From, To + 1);
        }

        public static string GenerateRandomMessage()
        {
            if (_MotivationalMessages == null || _MotivationalMessages.Rows.Count == 0)
            {
                _Logger.Log("Motivational data table cannot be empty.", EventLogEntryType.Error);
                throw new Exception("Motivational data table cannot be empty.");
            }
            return _MotivationalMessages.Rows[GetRandomNumber(0, _MotivationalMessages.Rows.Count)][0].ToString();
        }

        private static MimeMessage _GenerateMessage(string EmailFrom, string EmailTo, string Receiver, string Sender)
        {
            if (_MotivationalMessages == null || _MotivationalMessages.Rows.Count == 0)
            {
                _Logger.Log("Motivational data table cannot be empty.", EventLogEntryType.Error);
                throw new Exception("Motivational data table cannot be empty.");
            }

            MimeMessage Message = new MimeMessage();
            Message.From.Add(new MailboxAddress(Sender, EmailFrom));
            Message.To.Add(new MailboxAddress(Receiver, EmailTo));
            Message.Subject = "Have a great day!";
            Message.Body = new TextPart("plain")
            {
                Text = Receiver + ", " + GenerateRandomMessage()
            };
            return Message;
        }
        
        public static void SendMessage(string EmailFrom, string EmailTo, string Receiver, string Sender)
        {
            MimeMessage Message = _GenerateMessage(EmailFrom, EmailTo, Receiver, Sender);

            try
            { 
                using (var Client = new SmtpClient())
                {
                    Client.Connect(_Server, Convert.ToInt32(_PortNumber), MailKit.Security.SecureSocketOptions.StartTls);
                    Client.Authenticate(EmailFrom, _AuthenticationPassword);
                    _Logger.Log(Client.Send(Message), EventLogEntryType.Information); // Log the final response from the server.
                    Client.Disconnect(true);
                }
            }
            catch (Exception ex) 
            {
                _Logger.Log(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
