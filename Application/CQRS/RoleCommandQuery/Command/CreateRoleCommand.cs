using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.RoleCommandQuery.Command; 

public class CreateRoleCommand:IRequest<int>
{
    public string RoleName { get; set; }
    public bool IsActive { get; set; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IRoleRepository roleRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        this.roleRepository = roleRepository;
        this.unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = new()
        {
            RoleName = request.RoleName,
            IsActive = request.IsActive,
        };
        await roleRepository.AddRoleAsync(role);
        await unitOfWork.SaveChangesAsync();

        return role.Id;
   
     }
}
