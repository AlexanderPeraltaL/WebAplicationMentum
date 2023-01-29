using System.Data.SqlClient;

namespace WebAplicationMentum.Data
{
    public class DBHelper
    {
        private static SqlConnection _connection = null;
        private static readonly object _lock = new object();

        public static SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                lock (_lock)
                {
                    if (_connection == null)
                    {
                        _connection = new SqlConnection("Data Source=localhost;Initial Catalog=MentumApp;Integrated Security=True");
                    }
                }
            }

            return _connection;
        }
    }
}
