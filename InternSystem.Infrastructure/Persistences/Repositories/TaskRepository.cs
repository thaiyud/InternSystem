using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Infrastructure.Persistences.Repositories
{
        public class TaskRepository : BaseRepository<Tasks>, ITaskRepository
        {
            private readonly ApplicationDbContext _applicationDbContext;

            public TaskRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<Tasks>> GetTasksByNameAsync(string task)
            {
            string searchTerm = task.Trim().ToLower();

            return await _applicationDbContext.Tasks
                .Where(t => t.MoTa.ToLower().Trim().Contains(searchTerm))
                .ToListAsync();
        }
        public async Task<IEnumerable<Tasks>> GetTaskByDuanIdAsync(int id)
        {
            return await _applicationDbContext.Tasks
                .Where(t => t.DuAnId == id).ToListAsync();
        }

    }
    }
