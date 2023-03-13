using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdunnoAPI.DAL.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly IdunnoDbContext _context;
        private bool disposedValue;

        public UserRepository(IdunnoDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            User searchedUser = await _context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();

            if(searchedUser == null) 
            {
                throw new RequestException(StatusCodes.Status404NotFound, "This user could not be found.");
            }

            return searchedUser;
        }

        public async Task<string> GetUserNameAsync(int userId)  // revealing and throwing around users usernames is probably not an good idea -> just for the sake of demo project...
        {
            User searchedUser = await GetUserByIdAsync(userId);

            return searchedUser.Username;
        }

        public async Task<bool> CheckIfExists(User user)
        {
            User searchedUser = await _context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();

            if (searchedUser == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "This user could not be found.");
            }

            user.UserId = searchedUser.UserId; // setting id by reference, will be handy in many situations -> token generation is an example.

            return true;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await CheckIfExists(user);
            }catch(RequestException) // will occur only when user could not be found.
            {
                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return true;
            }

            throw new RequestException(StatusCodes.Status409Conflict, "Entered login already exists.");
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(_context != null)
                    {
                        _context.Dispose();
                    }
                    
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
