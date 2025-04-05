using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.UserCommandQuery.Command; 

public class CreateUserCommand :IRequest<int>
{
  
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
}


public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{

    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateUserCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }
    public Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
