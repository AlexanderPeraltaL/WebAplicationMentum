using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerContactUpdate
    {
        private string query = @"UPDATE CUSTOMERS_CONTACTS SET 
                                FULL_NAME=@FULL_NAME, 
                                ADDRESS=@ADDRESS, 
                                NUMBER_PHONE=@NUMBER_PHONE,
                                BIRTHDAY = @BIRTHDAY
                                WHERE ID=@ID";

        private CustomerContactModel customerContactModel;

        public CustomerContactUpdate(CustomerContactModel customerContactModel)
        {
            this.customerContactModel = customerContactModel;
        }

        public int Process()
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@ID", customerContactModel.Id },
                    { "@FULL_NAME", customerContactModel.FullName },
                    { "@ADDRESS", customerContactModel.Address },
                    { "@NUMBER_PHONE", customerContactModel.NumberPhone },
                    { "@BIRTHDAY",Convert.ToDateTime(customerContactModel.Birthday).ToString("yyyy/MM/dd")}
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
