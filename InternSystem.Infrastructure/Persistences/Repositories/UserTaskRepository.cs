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
    public class UserTaskRepository : BaseRepository<UserTask>, IUserTaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserTaskRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<IEnumerable<UserTask>> GetUserTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserTask> GetUserTasksByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
