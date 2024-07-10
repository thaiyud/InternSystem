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
    public class GetLichPhongvanByIdHandler : IRequestHandler<GetLichPhongVanByIdQuery, GetLichPhongVanByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLichPhongvanByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetLichPhongVanByIdResponse> Handle(GetLichPhongVanByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                LichPhongVan? existingDA = await _unitOfWork.LichPhongVanRepository.GetByIdAsync(request.Id);
                if (existingDA == null || existingDA.IsDelete == true)
                    throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi");

                return _mapper.Map<GetLichPhongVanByIdResponse>(existingDA);
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
