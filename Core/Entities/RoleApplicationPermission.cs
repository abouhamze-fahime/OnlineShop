using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities; 

public class RoleApplicationPermission
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public required Role Role { get; set; }
    public int ApplicationPermissionId { get; set; }
    public required ApplicationPermission ApplicationPermission { get; set; }


}
