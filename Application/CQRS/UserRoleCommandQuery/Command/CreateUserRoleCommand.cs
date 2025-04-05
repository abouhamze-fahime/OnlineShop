using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.UserRoleCommandQuery.Command;

public class CreateUserRoleCommand:IRequest<int>
{
    public int RoleId { get; set; }
    public Guid UserId { get; set; }
}
public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, int>
{

    private readonly IUserRoleRepository  userRoleRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateUserRoleCommandHandler(IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork)
    {
        this.userRoleRepository = userRoleRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        UserRole userRole = new()
        {
            UserId=request.UserId , 
            RoleId=request.RoleId ,
        };
        var result = await userRoleRepository.AddAsync(userRole);  
        await unitOfWork.SaveChangesAsync();
        return result.Id;
    }
}
