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
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            User searchedUser = await _context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();

            if(searchedUser == null) 
            {
                throw new RequestException(StatusCodes.Status404NotFound, "This user could not be found.");
            }

            return searchedUser;
        }

        public async Task<bool> CheckIfExists(User user)
        {
            User searchedUser = await _context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();

            if(searchedUser != null) // setting id by reference, will be handy in many situations like token generation for an example
            {
                user.UserId = searchedUser.UserId;
            }
            

            return searchedUser != null;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            if(await CheckIfExists(user))
            {
                throw new RequestException(StatusCodes.Status409Conflict, "Entered login already exists.");
            }

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

    }
}
