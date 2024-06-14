
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace InternSystem.Infrastructure.Persistences.Repositories
{
    public class InternInfoRepository : BaseRepository<InternInfo>, IInternInfoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InternInfoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<IEnumerable<InternInfo>> GetInternInfoByTenTruongHocAsync(string truongHocName)
        {
            var searchTerm = truongHocName.Trim().ToLower();
            return await _dbContext.InternInfos
                .Include(ii => ii.TruongHoc)
                .Where(ii => ii.TruongHoc.Ten.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<int> GetTotalInternStudentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var totalInternStudents = await _dbContext.InternInfos
                .Where(x => x.StartDate >= startDate && x.EndDate <= endDate)
                .CountAsync();

            return totalInternStudents;
        }

        public async Task<IEnumerable<InternInfo>> GetFilterdInternInfosAsync(int? schoolId, DateTime? startDate, DateTime? endDate)
        {
            var query = _dbContext.InternInfos.AsQueryable();

            if (schoolId.HasValue)
            {
                query = query.Where(i => i.IdTruong == schoolId);
            }

            if (startDate.HasValue)
            {
                // Set the time part of startDate to 00:00:00
                var startOfDay = startDate.Value.Date;
                query = query.Where(i => i.StartDate >= startOfDay);
            }

            if (endDate.HasValue)
            {
                // Set the time part of endDate to 23:59:59
                var endOfDay = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(i => i.EndDate <= endOfDay);
            }

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<GetInternInfoResponse<InternInfo>> GetInternInfosAsync(GetInternInfoQuery query)
        {
            var queryable = _dbContext.InternInfos.AsQueryable();
            if (!string.IsNullOrEmpty(query.HoTen))
                queryable = queryable.Where(ii => ii.HoTen.Contains(query.HoTen));
            if (query.NgaySinh.HasValue)
                queryable = queryable.Where(ii => ii.NgaySinh == query.NgaySinh);
            if (query.GioiTinh.HasValue)
                queryable = queryable.Where(ii => ii.GioiTinh == query.GioiTinh);
            if (!string.IsNullOrEmpty(query.MSSV))
                queryable = queryable.Where(ii => ii.MSSV.Contains(query.MSSV));
            if (!string.IsNullOrEmpty(query.EmailTruong))
                queryable = queryable.Where(ii => ii.EmailTruong.Contains(query.EmailTruong));
            if (!string.IsNullOrEmpty(query.EmailCaNhan))
                queryable = queryable.Where(ii => ii.EmailCaNhan.Contains(query.EmailCaNhan));
            if (!string.IsNullOrEmpty(query.Sdt))
                queryable = queryable.Where(ii => ii.Sdt.Contains(query.Sdt));
            if (!string.IsNullOrEmpty(query.DiaChi))
                queryable = queryable.Where(ii => ii.DiaChi.Contains(query.DiaChi));
            if (!string.IsNullOrEmpty(query.TruongHoc))
                queryable = queryable.Include(ii => ii.TruongHoc).Where(ii => ii.TruongHoc.Ten.Contains(query.TruongHoc));
            if (!string.IsNullOrEmpty(query.KyThucTap))
                queryable = queryable.Include(ii => ii.KyThucTap).Where(ii => ii.KyThucTap.Ten.Contains(query.KyThucTap));
            if (query.StartDate.HasValue)
                queryable = queryable.Where(ii => ii.StartDate >= query.StartDate);
            if (query.EndDate.HasValue)
                queryable = queryable.Where(ii => ii.EndDate <= query.EndDate);

            var totalCount = await queryable.CountAsync();
            var results = await queryable.ToListAsync();
            return new GetInternInfoResponse<InternInfo>(results, totalCount);
        }
        public async Task<List<GetInternStatsBySchoolIdResponse>> GetInternStatsBySchoolIdAsync(int schoolId)
        {
            var responseList = new List<GetInternStatsBySchoolIdResponse>();

            // Check if the school exists

            // Perform the query
            var result = await (
                from ktt in _dbContext.KyThucTaps
                where ktt.IdTruong == schoolId
                join intern in _dbContext.InternInfos on ktt.Id equals intern.KyThucTapId into internGroup
                from subIntern in internGroup.DefaultIfEmpty() // Ensures a left join
                group subIntern by new { ktt.Id, ktt.Ten } into grouped
                select new GetInternStatsBySchoolIdResponse
                {
                    KyThucTapId = grouped.Key.Id,
                    KyThucTapName = grouped.Key.Ten,
                    StudentCount = grouped.Count(i => i != null) // Count non-null interns
                }
            ).ToListAsync();

            return result;
        }
        public async Task<IEnumerable<InternInfo>> GetFilteredInternInfoByDaysAsync(DateTime? day)
        {
            if (day == null)
            {
                return new List<InternInfo>();
            }
            var dateOnly = day.Value.Date;

            var internsForInterviews = await (from lich in _dbContext.LichPhongVans
                                              join intern in _dbContext.InternInfos
                                              on lich.IdNguoiDuocPhongVan equals intern.Id
                                              where lich.ThoiGianPhongVan.Date == dateOnly
                                              select intern).ToListAsync();
            return internsForInterviews;
        }

        


        public async Task<int> GetAllInterning()
        {
            var currentDate = DateTime.Now;
            var interningList = await _dbContext.InternInfos.Where(x => x.StartDate <= currentDate && x.EndDate >= currentDate).ToListAsync();
            return interningList.Count;
        }

        public async Task<int> GetAllInterned()
        {
            var internedList = await _dbContext.InternInfos.ToListAsync();
            return internedList.Count;
        }

        public async Task<int> GetAllReceivedCV()
        {
            var receivedCVList = await _dbContext.InternInfos.Where(x => x.LinkCv != null).ToListAsync();
            return receivedCVList.Count;
        }
    }
}
