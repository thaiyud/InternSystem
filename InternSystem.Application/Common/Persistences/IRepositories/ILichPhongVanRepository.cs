using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface ILichPhongVanRepository : IBaseRepository<LichPhongVan>
    {
        Task<IEnumerable<LichPhongVan>> GetLichPhongVanByToday();
        Task<IEnumerable<LichPhongVan>> GetAllLichPhongVan();
        Task UpdateAsync(LichPhongVan updatedLPV);
    }
}
