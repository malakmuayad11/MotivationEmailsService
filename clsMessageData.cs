using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MotivationEmailsService
{
    public static class clsMessageData
    {
        private static string _ConnectionString;
        private static clsLogger _Logger;

        static clsMessageData()
        {
            _Logger = new clsLogger();
            _ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            if (_ConnectionString == null)
                _Logger.Log("Connection string cannot be empty. The service is stopped.", EventLogEntryType.Error);
        }

        public static DataTable GetAllMessages()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMessages", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                                dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.Log(ex.Message, EventLogEntryType.Error);
            }
            return dataTable;
        }
    }
}
