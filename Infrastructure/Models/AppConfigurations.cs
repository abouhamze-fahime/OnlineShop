using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AppConfigurations
    {
        public string TokenKey { get; set; }
        public int TokenTimeOut { get; set; }
        public int RefreshTokenTimeout { get; set; }
    }
}
