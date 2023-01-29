using System.Data.SqlClient;
using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerContactSelectById
    {
        private string query = @"SELECT *
                                FROM CUSTOMERS_CONTACTS
                                WHERE ID = @ID";
        private int id;
        public CustomerContactSelectById(int id)
        {
            this.id = id;
        }

        public CustomerContactModel Process()
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@ID", id }
                };

                SqlDml sqlDml = new SqlDml(values, query);
                SqlDataReader sqlDataReader = sqlDml.Process();
                if (sqlDataReader.Read())
                {
                    CustomerContactModel customer = new CustomerContactModel
                    {
                        Id = (int)sqlDataReader["ID"],
                        FullName = sqlDataReader["FULL_NAME"].ToString(),
                        Address = sqlDataReader["ADDRESS"].ToString(),
                        NumberPhone = sqlDataReader["NUMBER_PHONE"].ToString()
                    };
                    sqlDataReader.Close();
                    SqlConnectionData.connection.Close();
                    return customer;
                }
                else
                {
                    sqlDataReader.Close();
                    SqlConnectionData.connection.Close();
                    return null;
                }
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
