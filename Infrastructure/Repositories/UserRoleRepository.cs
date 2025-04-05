using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    private readonly IUnitOfWork unitOfWork;

    public UserRoleRepository (OnlineShopDbContext onlineShopDbContext, IUnitOfWork unitOfWork)
    {
        this.onlineShopDbContext = onlineShopDbContext;
        this.unitOfWork = unitOfWork;
    }
    public async Task<UserRole> AddAsync(UserRole userRole)
    {
       await  onlineShopDbContext.UserRoles.AddAsync(userRole);
        return userRole;
    }
}
