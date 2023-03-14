﻿using IdunnoAPI.Models;
using System.Linq.Expressions;

namespace IdunnoAPI.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IQueryable<User> GetUsersAsQueryable();
        Task<User> FindUserAsync(Expression<Func<User, bool>> predicate);
        Task<bool> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UpdateUserAsync(User user);
    }
}
