using System.Data.SqlClient;

namespace WebAplicationMentum.Data
{
    public static class SqlConnectionData
    {
        public static SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=MentumApp;Integrated Security=True");
    }
}
