using System.Net.Http.Headers;
using Infrastructure.Dto;
using Infrastructure.Models;

namespace Application.Interfaces;
public interface IProductService
{

    Task<ServiceResponse< List<ProductDto>>> GetAllAsync(int page=1 , int size=5);
    Task<ProductDto> GetAsync(int id);
    Task<Product> AddAsync(ProductDto product);
}