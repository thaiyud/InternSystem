using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface IDashboardRepository : IBaseRepository<Dashboard>
    {
        Task UpdateAsync(Dashboard dashboard);
    }
}
