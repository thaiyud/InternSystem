using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface IReportTaskRepository : IBaseRepository<ReportTask>
    {
        Task<IEnumerable<ReportTask>> GetReportTasksAsync();
        Task<ReportTask> GetReportTasksByIdAsync(int id);
    }

}
