﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities; 

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    public bool IsActive { get; set; }

    //navigation properties 
    public ICollection<RolePermission> RolePermissions { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
