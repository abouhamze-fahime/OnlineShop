using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ApplicationPermissionCommadnQuery.Command; 

public class CreatePermissionCommand :IRequest<int>
{
    public string Title { get; set; }
    public string PermissionFlag { get; set; }
    
}

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
{
    private readonly IPermissionRepository  permissionRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreatePermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
    {
        this.permissionRepository = permissionRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        Permission newPermission = new()
        {
            Title = request.Title,
            PermissionFlag = request.PermissionFlag,
        };
        await permissionRepository.AddAsync(newPermission);
        await unitOfWork.SaveChangesAsync();
        return newPermission.Id;
    }
}
