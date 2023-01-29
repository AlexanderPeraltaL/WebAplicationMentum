using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;
using WebAplicationMentum.Data;
using WebAplicationMentum.Models;
using System.Data.SqlClient;

namespace WebAplicationMentum.Business
{
    public class CustomerContactDelete
    {
        private string query = @"DELETE
                                FROM CUSTOMERS_CONTACTS
                                WHERE ID = @ID";
        private int id;
        public CustomerContactDelete(int id) 
        { 
            this.id = id;
        } 
        public int Process()
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>
                {
                    { "@ID", id }
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
