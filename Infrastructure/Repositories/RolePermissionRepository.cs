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

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    private readonly IUnitOfWork unitOfWork;

    public RolePermissionRepository(OnlineShopDbContext onlineShopDbContext, IUnitOfWork unitOfWork)
    {
        this.onlineShopDbContext = onlineShopDbContext;
        this.unitOfWork = unitOfWork;
    }
    public async Task<RolePermission> AddAsync(RolePermission permission)
    {
        await onlineShopDbContext.RolePermission.AddAsync(permission);
        return permission;
    }

    public async Task<List<string>> GetAllPermissionByRoleIdAsync(List<int> roleIds )
    {
        var permissionIds = await onlineShopDbContext.RolePermission
            .Where(p=> roleIds.Contains(p.RoleId))
            .Select(p=>p.Permission.PermissionFlag)
            .ToListAsync();
        return permissionIds;

    }
}
