using Azure;
using IdunnoAPI.Auth;
using IdunnoAPI.Auth.Interfaces;
using IdunnoAPI.DAL.Repositories;
using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace IdunnoAPI.DAL.Services
{
    public class UsersService : IUsersService, IDisposable
    {
        private bool disposedValue;
        public IUserRepository Users { get; }
        private readonly IJWToken _tk;


        public UsersService(IUserRepository users, IJWToken tokenGenerator) 
        {
            Users = users;
            _tk = tokenGenerator;
        }


        public async Task<string> AuthenticateUser(User user, HttpResponse response)
        {
            User foundUser = await Users.FindUserAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (foundUser == null) throw new RequestException(StatusCodes.Status401Unauthorized, "Provided credentials are invalid.");

            string token = _tk.GenerateToken(user);

            _tk.SpreadToken(token, response);

            return token;
        }

        public async Task<string> GetUserNameAsync(int userId)
        {
            User foundUser = await Users.FindUserAsync(u => u.UserId == userId);

            if (foundUser == null) throw new RequestException(StatusCodes.Status404NotFound, "User could not be found.");

            return foundUser.Username;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                 if(Users!= null)
                    {
                        Users.Dispose();
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
