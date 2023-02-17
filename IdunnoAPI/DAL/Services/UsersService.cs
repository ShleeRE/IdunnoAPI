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
        public IUserRepository Users { get;}

        public UsersService(IdunnoDbContext context) 
        {
            Users = new UserRepository(context);
        }

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

        public async Task<JwtSecurityToken> AuthenticateUser(User user)
        {
            bool found = await Users.CheckIfExists(user);

            if (!found) { throw new RequestException(StatusCodes.Status404NotFound, "User has been not found."); }




            throw new NotImplementedException();
        }
    }
}
