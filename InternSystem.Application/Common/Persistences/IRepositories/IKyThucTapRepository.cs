using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Domain.Entities;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface IKyThucTapRepository : IBaseRepository<KyThucTap>
    {
        Task<IEnumerable<KyThucTap>> GetKyThucTapsByNameAsync(string ten);
    }
}
