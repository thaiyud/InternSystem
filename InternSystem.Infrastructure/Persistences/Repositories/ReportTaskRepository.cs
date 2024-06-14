using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Infrastructure.Persistences.Repositories
{
    public class ReportTaskRepository : BaseRepository<ReportTask>, IReportTaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReportTaskRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<IEnumerable<ReportTask>> GetReportTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReportTask> GetReportTasksByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


    }
}
