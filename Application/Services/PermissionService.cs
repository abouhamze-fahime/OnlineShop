using Application.Interfaces;
using Core.IRepositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IUserRoleRepository userRole;
    private readonly IRolePermissionRepository rolePermission;
    private readonly IMemoryCache memoryCache;

    public PermissionService(
        IUserRoleRepository userRole,
        IRolePermissionRepository rolePermission,
        IMemoryCache memoryCache
        )
    {
        this.userRole = userRole;
        this.rolePermission = rolePermission;
        this.memoryCache = memoryCache;
    }


    public async Task<bool> CheckPermission(Guid userId, string permissionFlag)
    {
        string permissionCacheKey = $"permissions-{userId.ToString()}";

        // approach one

        var permissionFlags = new List<string>();

        if (!memoryCache.TryGetValue(permissionCacheKey, out permissionFlags))
        {

            //GetRoleQuery(userId);
            var roles = await userRole.GetRolesByUserIdAsync(userId);
            if (roles == null)
            {
                return false;
            }
            //GetAllPermissionQuery(roles);
            permissionFlags = await rolePermission.GetAllPermissionByRoleIdAsync(roles);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                      .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            memoryCache.Set(permissionCacheKey, permissionFlags, cacheEntryOptions);
        }

       // memoryCache.Remove(permissionCacheKey);
       // memoryCache.Dispose();


        return permissionFlags.Contains(permissionFlag);

    }
}
