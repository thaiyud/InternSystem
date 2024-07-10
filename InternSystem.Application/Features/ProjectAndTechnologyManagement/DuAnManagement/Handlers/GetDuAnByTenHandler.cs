using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Queries;
using InternSystem.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;

public class GetDuAnsByTenQueryHandler : IRequestHandler<GetDuAnByTenQuery, IEnumerable<GetDuAnByTenResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDuAnsByTenQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetDuAnByTenResponse>> Handle(GetDuAnByTenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var DuAns = await _unitOfWork.DuAnRepository.GetDuAnsByTenAsync(request.Ten);
            if (DuAns == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy dự án");
            }
            return _mapper.Map<IEnumerable<GetDuAnByTenResponse>>(DuAns);
        }
        catch (ErrorException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra");
        }
    }
}
