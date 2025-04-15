using Core.IRepositories;
using Infrastructure.Interfaces;
using Infrastructure.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ProductCommandQuery.Command;

public class SaveProductCommand : IRequest<SaveProductCommandResponse>
{
    public string ProductName { get; set; }
    // public int CategoryId { get; set; }
    public long Price { get; set; }
    // public string Description { get; set; }

    public IFormFile Thumbnail { get; set; }
}

public class SaveProductCommandResponse
{
    public int ProductId { get; set; }
}


public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, SaveProductCommandResponse>
{
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly FileUtility file;

    public SaveProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        FileUtility file
        )
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
        this.file = file;
    }

    public async Task<SaveProductCommandResponse> Handle(SaveProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            ProductName = request.ProductName,
            Price = request.Price,
            //save into folders
            ThumbnailFileName = file.SaveFileInFolder(request.Thumbnail , nameof(Product), true), //request.Thumbnail.FileName,


            Thumbnail =file.EncryptFile(file.ConvertToByteArray(request.Thumbnail)) ,
            ThumbnailFileExtension = file.GetFileExtension(request.Thumbnail.FileName),
            FileSize = request.Thumbnail.Length
        };



        await productRepository.InsertAsync(product);
        var result = await unitOfWork.SaveChangesAsync();
        var response = new SaveProductCommandResponse
        {
            ProductId = product.Id
        };
        return response;
    }


}
