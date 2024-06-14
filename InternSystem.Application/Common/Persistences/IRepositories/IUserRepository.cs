using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Domain.Entities;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface IUserRepository : IBaseRepository<AspNetUser>
    {
        Task<IEnumerable<AspNetUser>> GetUsersByHoVaTenAsync(string hoVaTen);
        Task<AspNetUser> GetByIdAsync(string id);
        Task UpdateAsync(AspNetUser user);
        Task<AspNetUser> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateUserAsync(AspNetUser user);
    }
}
