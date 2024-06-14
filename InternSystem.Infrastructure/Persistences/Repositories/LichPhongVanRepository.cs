using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace InternSystem.Infrastructure.Persistences.Repositories
{
    public class LichPhongVanRepository : BaseRepository<LichPhongVan>, ILichPhongVanRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LichPhongVanRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<IEnumerable<LichPhongVan>> GetLichPhongVanByToday()
        {
            var today = DateTime.UtcNow.Date;

            return await _dbContext.LichPhongVans
                .Where(lpv => lpv.ThoiGianPhongVan.Date == today && lpv.IsActive == true && lpv.IsDelete == false)
                .ToListAsync();
        }
        public async Task<IEnumerable<LichPhongVan>> GetAllLichPhongVan()
        {
            return await _dbContext.LichPhongVans.ToListAsync();
        }
        public async Task UpdateAsync(LichPhongVan updatedLPV)
        {
            var existingLPV = await _dbContext.LichPhongVans.FindAsync(updatedLPV.Id) ?? throw new Exception("LichPhongVan not found");
            _dbContext.LichPhongVans.Update(existingLPV);
            //_dbContext.Entry(existingLPV).CurrentValues.SetValues(updatedLPV);
            await _dbContext.SaveChangesAsync();
        }

    }
}
