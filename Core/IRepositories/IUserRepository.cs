using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userName);
        Task<List<User>> GetAllAsync();
        Task<Guid> AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<int> UpdateUserAsync(User user);
    }
}
