using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerUpdate
    {
        private string query = @"UPDATE CUSTOMERS 
                                SET FULL_NAME = @FULL_NAME, 
                                ADDRESS = @ADDRESS, 
                                NUMBER_PHONE = @NUMBER_PHONE
                                WHERE ID = @ID";
        private CustomersModel customersModel;
        public CustomerUpdate(CustomersModel customersModel)
        {
            this.customersModel = customersModel;
        }

        public int Process()
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    {"@Id",customersModel.Id },
                    {"@FULL_NAME", customersModel.FullName },
                    {"@ADDRESS", customersModel.Address },
                    {"@NUMBER_PHONE", customersModel.NumberPhone }
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
