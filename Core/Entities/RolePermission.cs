﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities; 

public class RolePermission
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public  Role Role { get; set; }
    public int PermissionId { get; set; }
    public  Permission Permission { get; set; }


}
