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
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly IUnitOfWork unitOfWork;

        public UserRefreshTokenRepository(OnlineShopDbContext onlineShopDbContext, IUnitOfWork unitOfWork)
        {
            this.onlineShopDbContext = onlineShopDbContext;
            this.unitOfWork = unitOfWork;
        }
        public async Task<UserRefreshToken> GetUserRefreshToken( Guid userId)
        {
            var userRefreshToken = await onlineShopDbContext.UserRefreshTokens
                .SingleOrDefaultAsync(u=>  u.UserId==userId);

            return userRefreshToken;
        }


        public async Task<UserRefreshToken> GetUserRefreshToken(string refreshToken)
        {
            var userRefreshToken = await onlineShopDbContext.UserRefreshTokens
                .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            return userRefreshToken;
        }

        public async Task<UserRefreshToken> InsertAsync (UserRefreshToken userRefreshToken)
        {
            await onlineShopDbContext.UserRefreshTokens.AddAsync(userRefreshToken);
            await onlineShopDbContext.SaveChangesAsync();
            return userRefreshToken;
        }


        public async Task UpdateRefreshTokenAsync()
        { 
            await onlineShopDbContext.SaveChangesAsync();
        }
    }
}
