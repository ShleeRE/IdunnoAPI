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
        private readonly ITokenGenerator _tkGenerator;


        public UsersService(IUserRepository users, ITokenGenerator tokenGenerator) 
        {
            Users = users;
            _tkGenerator = tokenGenerator;
        }


        public async Task<string> AuthenticateUser(User user)
        {
            bool found = await Users.CheckIfExists(user);

            if (!found) { throw new RequestException(StatusCodes.Status404NotFound, "User has been not found."); }

            string token = _tkGenerator.GenerateToken(user);

            return token;
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
