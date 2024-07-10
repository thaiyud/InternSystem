using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.TruongHocManagement.Models;
using InternSystem.Application.Features.InternManagement.TruongHocManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.TruongHocManagement.Handlers
{
    public class GetAllTruongHocHandler : IRequestHandler<GetAllTruongHocQuery, IEnumerable<GetAllTruongHocResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllTruongHocHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllTruongHocResponse>> Handle(GetAllTruongHocQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<TruongHoc> allTruongHoc = _unitOfWork.TruongHocRepository.Entities;
                IQueryable<TruongHoc> activeTruongHoc = allTruongHoc.Where(p => !p.IsDelete).OrderByDescending(p => p.Ten);
                if(!activeTruongHoc.Any()) 
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy trường học");
                var allTruongHocList = await _unitOfWork.TruongHocRepository.GetAllAsync()
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy trường học");

                return _mapper.Map<IEnumerable<GetAllTruongHocResponse>>(activeTruongHoc);
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
