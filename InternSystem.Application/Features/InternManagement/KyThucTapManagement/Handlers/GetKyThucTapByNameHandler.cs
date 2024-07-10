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
    public class GetKyThucTapByNameHandler : IRequestHandler<GetKyThucTapByNameQuery, IEnumerable<GetKyThucTapByNameResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetKyThucTapByNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetKyThucTapByNameResponse>> Handle(GetKyThucTapByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var kyThucTapList = await _unitOfWork.KyThucTapRepository.GetKyThucTapsByNameAsync(request.Ten);

                if (kyThucTapList == null || !kyThucTapList.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, $"Không tìm thấy kỳ thực tập có tên '{request.Ten}'");
                }

                var responses = new List<GetKyThucTapByNameResponse>();
                foreach (var kyThucTap in kyThucTapList)
                {
                    responses.Add(_mapper.Map<GetKyThucTapByNameResponse>(kyThucTap));
                }
                return responses;
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
