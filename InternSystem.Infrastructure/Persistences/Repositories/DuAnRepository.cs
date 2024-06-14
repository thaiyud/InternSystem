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
    public class DuAnRepository : BaseRepository<DuAn> , IDuAnRepository
    {
        private readonly ApplicationDbContext _context;

        public DuAnRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task UpdateDuAnAsync(DuAn duAn)
        {
            _context.Entry(duAn).State = EntityState.Modified;
        }
    }
}
