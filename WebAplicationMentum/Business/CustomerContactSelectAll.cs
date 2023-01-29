using System.Data.SqlClient;
using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerContactSelectAll
    {
        private readonly int id;
        private string query = @"SELECT CC.ID, CC.CUSTOMER_ID, CC.FULL_NAME, CC.ADDRESS, CC.NUMBER_PHONE FROM CUSTOMERS_CONTACTS AS CC
                                INNER JOIN CUSTOMERS AS C ON C.ID = CC.CUSTOMER_ID
                                WHERE C.ID = @ID";

        public CustomerContactSelectAll(int id)
        {
            this.id = id;
        }
        public List<CustomerContactModel> Process()
        {
            try
            {
                List<CustomerContactModel> customerContacts = new List<CustomerContactModel>();

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("@ID", id);

                SqlDml sqlDml = new SqlDml(values, query);
                SqlDataReader sqlData = sqlDml.Process();

                while (sqlData.Read())
                {
                    customerContacts.Add(new CustomerContactModel()
                    {
                        Id = Convert.ToInt32(sqlData["ID"]),
                        FullName = sqlData["FULL_NAME"].ToString(),
                        Address = sqlData["ADDRESS"].ToString(),
                        NumberPhone = sqlData["NUMBER_PHONE"].ToString(),
                        customersModel = new CustomersModel { Id = Convert.ToInt32(sqlData["CUSTOMER_ID"]) }
                    });
                }

                SqlConnectionData.connection.Close();
                return customerContacts;
            }
            catch (Exception ex)
            {
                SqlConnectionData.connection.Close();
                EventLog.WriteEntry("WebAplicationMentum", ex.ToString(), EventLogEntryType.Error);
                return null;
            }
        }
    }
}
