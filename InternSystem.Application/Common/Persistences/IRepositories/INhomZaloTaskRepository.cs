using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface INhomZaloTaskRepository : IBaseRepository<NhomZaloTask>
    {
        Task<IEnumerable<NhomZaloTask>> GetTaskByNhomZaloIdAsync(int id);
        public Task<NhomZaloTask> GetByNhomZaloIdAndTaskIdAsync(int nhomZaloid, int taskId);
    }
}
