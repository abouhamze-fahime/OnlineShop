using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ApplicationPermissionCommadnQuery.Query; 

public class GetAllPermissionQuery :IRequest<List<Permission>>
{
}



public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, List<Permission>>
{
    private readonly IPermissionRepository permissionRepository;


    public GetAllPermissionQueryHandler(IPermissionRepository permissionRepository)
    {
        this.permissionRepository = permissionRepository;

    }
    public async Task<List<Permission>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        List<Permission> permissions = await permissionRepository.GetAllAsync();
   
        return permissions;
    }



   
   
}
