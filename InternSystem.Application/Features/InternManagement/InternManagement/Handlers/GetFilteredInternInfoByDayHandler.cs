using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.InternManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

public class GetFilteredInternInfoByDayHandler : IRequestHandler<GetFilteredInternInfoByDayQuery, IEnumerable<InternInfo>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFilteredInternInfoByDayHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<InternInfo>> Handle(GetFilteredInternInfoByDayQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var interns = await _unitOfWork.InternInfoRepository.GetFilteredInternInfoByDaysAsync(request.Day);
            if (interns == null || !interns.Any())
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy thông tin thực tập sinh theo ngày được chọn");
            }
            return interns;
        }
        catch (ErrorException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lưu.");
        }
    }
}