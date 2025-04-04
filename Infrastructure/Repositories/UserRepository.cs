using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly IUnitOfWork unitOfWork;

        public UserRepository(OnlineShopDbContext onlineShopDbContext, IUnitOfWork unitOfWork)
        {
            this.onlineShopDbContext = onlineShopDbContext;
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserAsync(string userName)
        {
            var user = await onlineShopDbContext.Users.SingleOrDefaultAsync(u=>u.UserName==userName);
            if (user == null) 
            {
                throw new Exception();
            }
            return user;
        }

   
        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }




        public async Task<Guid> AddUserAsync(User user)
        {
            await onlineShopDbContext.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            return user.Id;
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

     

        public Task<int> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
