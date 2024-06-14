using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace InternSystem.Infrastructure.Persistences.Repositories;

public class UserRepository : BaseRepository<AspNetUser>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AspNetUser>> GetUsersByHoVaTenAsync(string hoVaTen)
    {
        var searchTerm = hoVaTen.Trim().ToLower();
        var users = await _dbContext.Users
                               .Where(u => u.HoVaTen.ToLower().Contains(searchTerm))
                               .ToListAsync();
        return users;
    }

    public async Task<AspNetUser> GetByIdAsync(string id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task UpdateAsync(AspNetUser user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }



    public async Task<AspNetUser> GetUserByRefreshTokenAsync(string resetToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.ResetToken == resetToken);

    }

    public async Task UpdateUserAsync(AspNetUser user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}