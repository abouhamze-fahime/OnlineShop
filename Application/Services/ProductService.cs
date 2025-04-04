
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;
public class ProductService : IProductService
{
    private readonly OnlineShopDbContext _context;
    private readonly IMapper _mapper;
    public ProductService(OnlineShopDbContext context ,IMapper mapper)
    {
        _context=context;
        _mapper=mapper;
    }
    public async Task<List<ProductDto>> GetAllAsync()
    {
       var productList= await _context.Products.ToListAsync();
       List<ProductDto> result = _mapper.Map<List<ProductDto>>(productList );
       return result;
    }
    public async Task<ProductDto> GetAsync(int id)
    {
       Product? pro = await _context.Products.FindAsync(id);
       ProductDto result = _mapper.Map<ProductDto>(pro);
       return result;
    }

    public async Task<Product> AddAsync(ProductDto newProduct)
    {
       Product product = _mapper.Map<Product>(newProduct);
       await _context.Products.AddAsync(product);
       await _context.SaveChangesAsync();
       return product;
    }
}