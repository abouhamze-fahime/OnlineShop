using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<int>> GetRolesByUserIdAsync(Guid userId)
    {
        var roleIds = await onlineShopDbContext.UserRoles
            .Where(r=>r.UserId==userId && r.Role.IsActive)
            .Select(r=>r.RoleId).ToListAsync();
        return roleIds;
    }
}
