using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class ApplicationPermission
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public Application Application { get; set; }
    public string Permission { get; set; }
    public DateTime CreateDate { get; set; }
}


