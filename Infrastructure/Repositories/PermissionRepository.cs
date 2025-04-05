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

public class PermissionRepository : IPermissionRepository
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    private readonly IUnitOfWork unitOfWork;

    public PermissionRepository(OnlineShopDbContext onlineShopDbContext, IUnitOfWork unitOfWork)
    {
        this.onlineShopDbContext = onlineShopDbContext;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Permission> AddAsync(Permission applicationPermission)
    {
        await onlineShopDbContext.AddAsync(applicationPermission);
        return applicationPermission;
    }

  

    public async Task<List<Permission>> GetAllAsync()
    {
      var result =  await onlineShopDbContext.Permissions.AsNoTracking().ToListAsync();
      return result;
    }
}
