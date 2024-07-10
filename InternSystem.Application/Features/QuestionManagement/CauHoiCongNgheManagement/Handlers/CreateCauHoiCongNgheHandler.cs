﻿using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Application.Features.QuestionManagement.CauHoiCongNgheManagement.Commands;
using InternSystem.Application.Features.QuestionManagement.CauHoiCongNgheManagement.Models;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace InternSystem.Application.Features.QuestionManagement.CauHoiCongNgheManagement.Handlers
{
    public class CreateCauHoiCongNgheHandler : IRequestHandler<CreateCauHoiCongNgheCommand, CreateCauHoiCongNgheResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public CreateCauHoiCongNgheHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<CreateCauHoiCongNgheResponse> Handle(CreateCauHoiCongNgheCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CauHoi? cauHoi = await _unitOfWork.CauHoiRepository.GetByIdAsync(request.IdCauHoi)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Câu hỏi không tồn tại.");

                CongNghe? congNghe = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.IdCongNghe)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Công nghệ không tồn tại.");


                CauHoiCongNghe? cauHoiCongNghe = _mapper.Map<CauHoiCongNghe>(request);
                cauHoiCongNghe.CreatedBy = _userContextService.GetCurrentUserId();
                cauHoiCongNghe.LastUpdatedBy = cauHoiCongNghe.CreatedBy;
                cauHoiCongNghe.CreatedTime = _timeService.SystemTimeNow;
                cauHoiCongNghe.LastUpdatedTime = _timeService.SystemTimeNow;

                cauHoiCongNghe = await _unitOfWork.CauHoiCongNgheRepository.AddAsync(cauHoiCongNghe);
                await _unitOfWork.SaveChangeAsync();

                return _mapper.Map<CreateCauHoiCongNgheResponse>(cauHoiCongNghe);
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
}
