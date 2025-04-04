using System.Net.Http.Headers;
using Infrastructure.Dto;

namespace Application.Interfaces;
public interface IProductService
{

    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> GetAsync(int id);
    Task<Product> AddAsync(ProductDto product);
}