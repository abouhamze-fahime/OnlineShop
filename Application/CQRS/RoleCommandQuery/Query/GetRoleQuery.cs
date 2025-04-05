using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RoleCommandQuery.Query; 

public class GetRoleQuery:IRequest<List<Role>>
{

}

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<Role>>
{
    private readonly IRoleRepository roleRepository;
   

    public GetRoleQueryHandler(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
        
    }
    public async Task<List<Role>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var result = await roleRepository.GetAllRolesAsync();
        return result;
    }
}
