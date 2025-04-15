using AutoMapper;
using Core.IRepositories;
using Infrastructure.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ProductCommandQuery.Query;

public class GetProductQuery : IRequest<GetProductQueryResponse>
{
    public int Id { get; set; }
}

public class GetProductQueryResponse
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string? PriceWithComma { get; set; }
    public string ThumbnailBase64 { get; set; }
    public IFormFile Thumbnail { get; set; }
    public string ThumbnailUrl { get; set; }

}

public class ProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    private readonly FileUtility file;

    public ProductQueryHandler(
        IProductRepository productRepository, 
        IMapper mapper,
        FileUtility file
        )
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        this.file = file;
    }
    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetAsync(request.Id);

        GetProductQueryResponse result = new()
        {
            Id = product.Id,
            Title = product.ProductName,
            PriceWithComma = product.Price.ToString("N0"),
            ThumbnailBase64 = file.ConvertToBase64(file.Decrypt(product.Thumbnail)),
            // ThumbnailUrl = file.GetFileUrl(product.ThumbnailFileName, nameof(Product)),
            ThumbnailUrl = file.GetEncryptedFileActionUrl(product.ThumbnailFileName, nameof(Product)),
        };
        return result;

    }
}
