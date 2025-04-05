using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RolePermissionCommandQuery.Command;

public class CreateRolePermissionCommand :IRequest<int>
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}
public class CreateRolePermissionCommandHandler : IRequestHandler<CreateRolePermissionCommand, int>
{
    private readonly IRolePermissionRepository  rolePermissionRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateRolePermissionCommandHandler(IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork)
    {
        this.rolePermissionRepository = rolePermissionRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        RolePermission rolePermission = new () 
        { 
            PermissionId = request.PermissionId,
            RoleId =request.RoleId,
        };
        var newRolePermission = await rolePermissionRepository.AddAsync(rolePermission);
        await  unitOfWork.SaveChangesAsync();
        return rolePermission.Id;
    }
}
