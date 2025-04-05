using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories;

public interface IApplicationRepository
{
    Task<Application> CreateAsync(Application application);
}
