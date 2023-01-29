using System.Data.SqlClient;

namespace WebAplicationMentum.Data
{
    public class SqlDml
    {
        private readonly Dictionary<string, object> values;
        private readonly string query;
        public int rowsAffected;

        public SqlDml(Dictionary<string, object> keyValues, string query)
        {
            this.values = keyValues;
            this.query = query;
        }
        public SqlDataReader Process()
        {
            SqlConnectionData.connection.Open();
            SqlCommand sqlCommand = new(query, SqlConnectionData.connection);

            foreach (var value in values)
            {
                sqlCommand.Parameters.AddWithValue(value.Key, Convert.ToString(value.Value));
            }
            SqlDataReader sqlData = sqlCommand.ExecuteReader();
            rowsAffected = sqlData.RecordsAffected;
            return sqlData;
        }
    }
}
