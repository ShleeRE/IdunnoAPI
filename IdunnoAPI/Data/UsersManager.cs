using IdunnoAPI.Models;
using MySqlConnector;

namespace IdunnoAPI.Data
{
    public class UsersManager
    {
        private readonly MySqlDbContext _context;

        public UsersManager(MySqlDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FindUserAsync(User user)
        {
            try
            {
                await _context.conn.OpenAsync();
                using MySqlCommand mySqlCommand = _context.conn.CreateCommand();

                mySqlCommand.CommandText = $"USE idunnodb; " +
                    $"SELECT * " +
                    $"FROM Users " +
                    $"WHERE UserLogin = '{user.Username}' AND UserPassword = '{user.Password}';";
                
                await using MySqlDataReader reader = await mySqlCommand.ExecuteReaderAsync();

                if(!reader.HasRows)
                {
                    return false;
                }

                return true;


            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
