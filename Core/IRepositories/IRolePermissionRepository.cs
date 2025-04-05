using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories;

public interface IRolePermissionRepository
{
    Task<RolePermission> AddAsync(RolePermission permission);

    Task<List<string>> GetAllPermissionByRoleIdAsync(List<int> roleIds);
}
