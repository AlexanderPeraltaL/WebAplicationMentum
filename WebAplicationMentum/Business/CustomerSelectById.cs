using System.Data.SqlClient;
using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerSelectById
    {
        private readonly int id;
        private const string query = "SELECT * FROM CUSTOMERS WHERE ID = @ID";

        public CustomerSelectById(int id)
        {
            this.id = id;
        }

        public CustomersModel Process()
        {
            try
            {
                CustomersModel customersModel = new CustomersModel();
                SqlDml sqlDml = new SqlDml(new Dictionary<string, object> { { "@ID", id } }, query);
                SqlDataReader sqlData = sqlDml.Process();

                if (sqlData.Read())
                {
                    customersModel.Id = Convert.ToInt32(sqlData["ID"]);
                    customersModel.FullName = sqlData["FULL_NAME"].ToString();
                    customersModel.Address = sqlData["ADDRESS"].ToString();
                    customersModel.NumberPhone = sqlData["NUMBER_PHONE"].ToString();
                    customersModel.DateCreation = Convert.ToDateTime(sqlData["DATE_CREATION"]);
                }
                SqlConnectionData.connection.Close();
                return customersModel;
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
