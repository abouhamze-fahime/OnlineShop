﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories; 

public interface IRoleRepository
{
    Task<Role> AddRoleAsync(Role role);
    Task<List<Role>> GetAllRolesAsync();
}
