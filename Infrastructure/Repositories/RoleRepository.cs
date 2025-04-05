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

public class RoleRepository : IRoleRepository
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    

    public RoleRepository(OnlineShopDbContext onlineShopDbContext)
    {
        this.onlineShopDbContext = onlineShopDbContext;
       
    }
    public async Task<Role> AddRoleAsync(Role role)
    {
        await  onlineShopDbContext.Roles.AddAsync(role);
        return role;
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        var result = await onlineShopDbContext.Roles.AsNoTracking().ToListAsync();
        return result;
    }
}
