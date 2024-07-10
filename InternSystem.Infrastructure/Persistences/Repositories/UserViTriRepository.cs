using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;

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
