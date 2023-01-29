using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerInsert
    {
        private string query = @"INSERT INTO CUSTOMERS(FULL_NAME, ADDRESS, NUMBER_PHONE, DATE_CREATION)
                                 VALUES(@FULL_NAME, @ADDRESS, @NUMBER_PHONE, @DATE_CREATION)";
        private CustomersModel customersModel;

        public CustomerInsert(CustomersModel customersModel)
        {
            this.customersModel = customersModel;
        }

        public int Process()
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@FULL_NAME", customersModel.FullName },
                    { "@ADDRESS", customersModel.Address },
                    { "@NUMBER_PHONE", customersModel.NumberPhone },
                    { "@DATE_CREATION", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
                };

                SqlDml sqlDml = new SqlDml(values, query);
                sqlDml.Process();
                SqlConnectionData.connection.Close();
                return sqlDml.rowsAffected;
            }
            catch (Exception ex)
            {
                SqlConnectionData.connection.Close();
                EventLog.WriteEntry("WebAplicationMentum", ex.ToString(), EventLogEntryType.Error);
                return 0;
            }
        }
    }
}
