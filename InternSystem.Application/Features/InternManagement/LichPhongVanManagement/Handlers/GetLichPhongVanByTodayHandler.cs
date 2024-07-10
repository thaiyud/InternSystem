using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Handlers
{
    public class GetLichPhongVanByTodayHandler : IRequestHandler<GetLichPhongVanByTodayQuery, IEnumerable<LichPhongVan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLichPhongVanByTodayHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LichPhongVan>> Handle(GetLichPhongVanByTodayQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lichPhongVanList = await _unitOfWork.LichPhongVanRepository.GetLichPhongVanByToday();
                if (lichPhongVanList == null || !lichPhongVanList.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không có lịch phỏng vấn trong hôm nay");
                }

                return lichPhongVanList;
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
}
