using MySqlConnector;

namespace IdunnoAPI.Data
{
    public class MySqlDbContext : IDisposable
    {
        MySqlConnection conn { get; }


        public MySqlDbContext(string connectionStr)
        {
            conn = new MySqlConnection(connectionStr);
        }

        public void Dispose()
        {
            conn.Dispose();
        }
    }
}
