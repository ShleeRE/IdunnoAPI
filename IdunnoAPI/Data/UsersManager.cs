using IdunnoAPI.Extensions;
using IdunnoAPI.Helpers;
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

        public async Task<ValidationResult> RegisterUserAsync(User user)
        {
            ValidationResult ret = new ValidationResult();

            try
            {
                ValidationResult foundResult = await FindUserAsync(user);

                _context.conn.Close();                                      // close connection in case if async function would be too slow   

                if (foundResult.Succeded)
                {
                    return ret.FormatReturn(false, "Used login already exists", StatusCodes.Status409Conflict);
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
                    return ret.RetInternelServerError();
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return ret.RetInternelServerError();
            }

            return ret.FormatReturn(true, "User've been registered!", StatusCodes.Status200OK);
        }
        public async Task<ValidationResult> FindUserAsync(User user)
        {
            ValidationResult ret = new ValidationResult();

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
                    ret.Succeded = false;
                    ret.Message = "User not found.";
                    ret.StatusCode = StatusCodes.Status404NotFound;

                    return ret;
                }

                return ret.FormatReturn(true, "User found", StatusCodes.Status200OK);


            }catch(Exception ex)
            {
                return ret.RetInternelServerError();
            }
        }

        public async Task<ValidationResult> DeleteUserAsync(int toBeDeleted)
        {
            ValidationResult ret = new ValidationResult();

            try
            {
                await _context.conn.OpenAsync();
                using MySqlCommand cmd = _context.conn.CreateCommand();
                cmd.CommandText = $"USE idunnodb; " +
                    $"DELETE FROM Users " +
                    $"WHERE UserID = '{toBeDeleted}'";

                if (await cmd.ExecuteNonQueryAsync() == -1)
                {
                    return ret.RetInternelServerError();
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return ret.RetInternelServerError();
            }

            return ret.FormatReturn(true, "User successfully deleted!", StatusCodes.Status200OK);
        }
    }
}
