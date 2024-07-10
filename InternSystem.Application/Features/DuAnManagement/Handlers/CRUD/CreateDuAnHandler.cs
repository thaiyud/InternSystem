using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.CustomExceptions;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Services.Interfaces;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Commands;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Models;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DuAnManagement.Handlers.CRUD
{
    public class CreateDuAnHandler : IRequestHandler<CreateDuAnCommand, CreateDuAnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;

        public CreateDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IUserContextService userContextService, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
            _timeService = timeService;
        }

        public async Task<CreateDuAnResponse> Handle(CreateDuAnCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DuAn? existingDA = _unitOfWork.DuAnRepository.GetAllAsync().Result.AsQueryable().FirstOrDefault(d => d.Ten.Equals(request.Ten));

                if (existingDA != null)
                    throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.NotUnique, "Trùng tên Dự án");

                var createdBy = _userContextService.GetCurrentUserId();
                if (createdBy.IsNullOrEmpty())
                    throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Không tìm thấy Id của người dùng hiện tại");

                DuAn newDA = _mapper.Map<DuAn>(request);
                newDA.CreatedBy = createdBy;
                newDA.CreatedTime = _timeService.SystemTimeNow;
                newDA.LastUpdatedTime = _timeService.SystemTimeNow;
                newDA.IsActive = true;
                newDA.IsDelete = false;
                newDA = await _unitOfWork.DuAnRepository.AddAsync(newDA);

                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<CreateDuAnResponse>(newDA);
            }
            catch (Exception)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã xảy ra lỗi không mong muốn khi lưu");
            }
        }
    }
}
