using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IUserRefreshTokenRepository
    {
        Task<UserRefreshToken> GetUserRefreshToken( Guid userId);
        Task<UserRefreshToken> InsertAsync(UserRefreshToken userRefreshToken);
        Task<UserRefreshToken> GetUserRefreshToken(string refreshToken);
        Task UpdateRefreshTokenAsync();
    }
}
