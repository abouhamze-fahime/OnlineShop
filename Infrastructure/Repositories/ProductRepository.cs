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
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly IUnitOfWork unitOfWork;

        public ProductRepository(OnlineShopDbContext onlineShopDbContext , IUnitOfWork unitOfWork)
        {
            this.onlineShopDbContext = onlineShopDbContext;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Product> GetAsync(int id)
        {
          var result = await onlineShopDbContext.Products.FindAsync( id);
            return result;

        }

        public async Task<List<Product>> GetAllAsync()
        {
            var result = await onlineShopDbContext.Products.ToListAsync();
            return result;
        }

        public async Task<int> InsertAsync(Product product)
        {

         await  onlineShopDbContext.Products.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
       //  await  onlineShopDbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
