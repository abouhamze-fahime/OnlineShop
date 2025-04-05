using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    private readonly IUnitOfWork unitOfWork;

    public ApplicationRepository(OnlineShopDbContext onlineShopDbContext, IUnitOfWork unitOfWork)
    {
        this.onlineShopDbContext = onlineShopDbContext;
        this.unitOfWork = unitOfWork;
    }
    public async Task<Application> CreateAsync(Application application)
    {
        await onlineShopDbContext.AddAsync(application);
        return application;
    }
}
