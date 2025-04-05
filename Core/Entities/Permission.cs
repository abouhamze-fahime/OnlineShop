﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Permission
{
    public int Id { get; set; }
    public string Title  { get; set; }
    public string PermissionFlag  { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}


