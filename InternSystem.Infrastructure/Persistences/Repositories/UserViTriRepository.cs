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
    internal class UserViTriRepository : BaseRepository<UserViTri>, IUserViTriRepository
    {
        private readonly ApplicationDbContext _context;

        public UserViTriRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
