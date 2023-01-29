using System.Data.SqlClient;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Business
{
    public class CustomerSelectAll
    {
        string query = @"SELECT * FROM CUSTOMERS";
        public CustomerSelectAll()
        {

        }
        public List<CustomersModel> Process()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            SqlDml sqlDml = new SqlDml(values, query);
            SqlDataReader sqlData = sqlDml.Process();
            List<CustomersModel> customers = new List<CustomersModel>();
            while (sqlData.Read())
            {
                customers.Add(new CustomersModel
                {
                    Id = (int)sqlData["ID"],
                    FullName = (string)sqlData["FULL_NAME"],
                    Address = (string)sqlData["ADDRESS"],
                    NumberPhone = (string)sqlData["NUMBER_PHONE"],
                    DateCreation = (DateTime)sqlData["DATE_CREATION"]
                });
            }
            SqlConnectionData.connection.Close();
            return customers;
        }
    }
}
