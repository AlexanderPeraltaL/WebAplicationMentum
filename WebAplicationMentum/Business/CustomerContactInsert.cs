using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerContactInsert
    {
        private string query = @"INSERT INTO CUSTOMERS_CONTACTS(FULL_NAME, ADDRESS, NUMBER_PHONE, CUSTOMER_ID, BIRTHDAY)
                                 VALUES(@FULL_NAME, @ADDRESS, @NUMBER_PHONE, @CUSTOMER_ID, @BIRTHDAY)";
        private CustomerContactModel customerContactModel;

        public CustomerContactInsert(CustomerContactModel customerContactsModel)
        {
            this.customerContactModel = customerContactsModel;
        }

        public int Process()
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@FULL_NAME", customerContactModel.FullName },
                    { "@ADDRESS", customerContactModel.Address },
                    { "@NUMBER_PHONE", customerContactModel.NumberPhone },
                    { "@CUSTOMER_ID", customerContactModel.customersModel.Id },
                    { "@BIRTHDAY", Convert.ToDateTime(customerContactModel.Birthday).ToString("yyyy/MM/dd")}
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
