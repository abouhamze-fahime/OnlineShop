
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;
public class ProductService : IProductService
{
    private readonly OnlineShopDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> logger;

    public ProductService(OnlineShopDbContext context, IMapper mapper ,ILogger<ProductService> logger)
    {
        _context = context;
        _mapper = mapper;
        this.logger = logger;
    }
    public async Task<ServiceResponse<List<ProductDto>>> GetAllAsync(int page = 1, int size = 5)
    {

        // paging 
        // pageNo , size 
        //.skip((page-1)* size).take(20)
        logger.LogInformation("Call GetAll from product service");
        var response = new ServiceResponse<List<ProductDto>>();
        try
        {
            var productList = await _context.Products
                     .Skip((page - 1) * size)
                     .Take(size)
                     .AsNoTracking()
                     .ToListAsync();
            List<ProductDto> result = _mapper.Map<List<ProductDto>>(productList);
            var totalRecords = await _context.Products.CountAsync();
            response.IsSuccess = true;
            response.Data = result;
            response.Page = page;
            response.Size = size;
            response.Total = totalRecords;
            logger.LogInformation("Get all was successfull");

        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }
    public async Task<ProductDto> GetAsync(int id)
    {
        Product? pro = await _context.Products.FindAsync(id);
        ProductDto result = _mapper.Map<ProductDto>(pro);
        return result;
    }

    public async Task<Product> AddAsync(ProductDto newProduct)
    {
        logger.LogInformation("Call Add from product service");
        Product product = _mapper.Map<Product>(newProduct);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
}