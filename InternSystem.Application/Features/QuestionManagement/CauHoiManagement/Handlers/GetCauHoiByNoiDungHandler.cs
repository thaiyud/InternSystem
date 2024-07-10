using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.QuestionManagement.CauHoiManagement.Models;
using InternSystem.Application.Features.QuestionManagement.CauHoiManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.QuestionManagement.CauHoiManagement.Handlers
{
    public class GetCauHoiByNoiDungHandler : IRequestHandler<GetCauHoiByNoiDungQuery, GetCauHoiByNoiDungResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCauHoiByNoiDungHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<GetCauHoiByNoiDungResponse> Handle(GetCauHoiByNoiDungQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CauHoi? existingCauHoi = (CauHoi?)(await _unitOfWork.CauHoiRepository.GetCauHoiByNoiDungAsync(request.noidung)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Câu hỏi không tồn tại."));
                var result = existingCauHoi.IsDelete == true
                    ? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy câu hỏi.")
                    : _mapper.Map<GetCauHoiByNoiDungResponse>(existingCauHoi);
                return result;
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
