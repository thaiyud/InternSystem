using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Domain.Entities;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface IInternInfoRepository : IBaseRepository<InternInfo>
    {
        Task<IEnumerable<InternInfo>> GetInternInfoByTenTruongHocAsync(string truongHocName);
        Task<int> GetTotalInternStudentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<InternInfo>> GetFilterdInternInfosAsync(int? schoolId, DateTime? startDate, DateTime? endDate);
        Task<List<GetInternStatsBySchoolIdResponse>> GetInternStatsBySchoolIdAsync(int schoolId);
        Task<GetInternInfoResponse<InternInfo>> GetInternInfosAsync(GetInternInfoQuery query);
        Task<IEnumerable<InternInfo>> GetFilteredInternInfoByDaysAsync(DateTime? day);
        Task<int> GetAllReceivedCV();
        Task<int> GetAllInterning();
        Task<int> GetAllInterned();
    }
}
