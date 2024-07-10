using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Models;
using InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Handlers
{
    public class GetAllLichPhongVanHandler : IRequestHandler<GetAllLichPhongVanQuery, IEnumerable<GetAllLichPhongVanResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLichPhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllLichPhongVanResponse>> Handle(GetAllLichPhongVanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<LichPhongVan> allLichPhongVan = _unitOfWork.LichPhongVanRepository.Entities;
                IQueryable<LichPhongVan> activeLichPhongVan = allLichPhongVan.Where(p => !p.IsDelete).OrderByDescending(p => p.CreatedTime);
                var allLichPhongVanList = await _unitOfWork.LichPhongVanRepository.GetAllLichPhongVan()
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy lịch phỏng vấn");

                return _mapper.Map<IEnumerable<GetAllLichPhongVanResponse>>(activeLichPhongVan);
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
