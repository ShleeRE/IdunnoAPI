using IdunnoAPI.Extensions;
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

        private ValidationResult ReturnInternalServerError()
        {
            ValidationResult ret = new ValidationResult();

            ret.Succeded = false;
            ret.Message = "Failed to register user - SERVER ERROR.";
            ret.StatusCode = StatusCodes.Status500InternalServerError;

            return ret;

        }

        public async Task<ValidationResult> RegisterUserAsync(User user)
        {
            ValidationResult ret = new ValidationResult();

            try
            {
                bool found = await FindUserAsync(user);

                _context.conn.Close();                                      // close connection in case if async function would be too slow   

                if (found)
                {
                    ret.Succeded = false;
                    ret.Message = "Used login already exists";
                    ret.StatusCode = StatusCodes.Status409Conflict;

                    return ret;
                }

                await _context.conn.OpenAsync();
                using MySqlCommand cmd = _context.conn.CreateCommand();

                cmd.CommandText = $"USE idunnodb; " +                       // 2 different queries, first one to find next userID
                    $"SELECT COUNT(*) " +
                    $"From Users;";

                await using MySqlDataReader reader = await cmd.ExecuteReaderAsync();

                await reader.ReadAsync();

                int retID = reader.GetInt32(0);

                retID += 1;

                await reader.DisposeAsync();

                cmd.CommandText = $"USE idunnodb; " +
                    $"INSERT INTO Users " +
                    $"VALUES ({retID}, '{user.Username}', '{user.Password}', 'User');";

                if (await cmd.ExecuteNonQueryAsync() == -1)
                {   
                    return ReturnInternalServerError();
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return ReturnInternalServerError();
            }

            ret.Succeded = true;
            ret.Message = "Success";
            ret.StatusCode = StatusCodes.Status201Created;

            return ret;
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

        public async Task<bool> DeleteUserAsync(int toBeDeleted)
        {
            try
            {
                await _context.conn.OpenAsync();
                using MySqlCommand cmd = _context.conn.CreateCommand();
                cmd.CommandText = $"USE idunnodb; " +
                    $"DELETE FROM Users " +
                    $"WHERE UserID = '{toBeDeleted}'";

                if (await cmd.ExecuteNonQueryAsync() == -1)
                {
                    return false;
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
