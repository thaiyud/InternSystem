using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.KyThucTapManagement.Models;
using InternSystem.Application.Features.InternManagement.KyThucTapManagement.Queries;
using InternSystem.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.KyThucTapManagement.Handlers
{
    public class GetAllKyThucTapHandler : IRequestHandler<GetAllKyThucTapQuery, IEnumerable<GetAllKyThucTapResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllKyThucTapHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllKyThucTapResponse>> Handle(GetAllKyThucTapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ListKyThucTap = await _unitOfWork.KyThucTapRepository.GetAllAsync();

                if (ListKyThucTap == null || !ListKyThucTap.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy kỳ thực tập nào");
                }

                return _mapper.Map<IEnumerable<GetAllKyThucTapResponse>>(ListKyThucTap);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra.");
            }
        }

    }
}
