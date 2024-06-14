using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface IUserNhomZaloRepository : IBaseRepository<UserNhomZalo>
    {
        Task<UserNhomZalo> GetByUserIdAndNhomZaloIdAsync(string userId, int nhomZaloId);
        Task UpdateUserNhomZaloAsync(UserNhomZalo userNhomZalo);
        Task<IEnumerable<UserNhomZalo>> GetByNhomZaloIdAsync(int nhomZaloId);
        Task<IEnumerable<UserNhomZalo>> GetByUserIdAsync(string userId);
    }
}
