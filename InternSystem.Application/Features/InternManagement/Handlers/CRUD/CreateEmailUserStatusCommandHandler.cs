using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Commands;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.Handlers.CRUD
{
    public class CreateEmailUserStatusCommandHandler : IRequestHandler<CreateEmailUserStatusCommand, GetDetailEmailUserStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateEmailUserStatusCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetDetailEmailUserStatusResponse> Handle(CreateEmailUserStatusCommand request, CancellationToken cancellationToken)
        {
            var status = _mapper.Map<EmailUserStatus>(request);

            var intern = await _unitOfWork.InternInfoRepository.GetByIdAsync(request.IdNguoiNhan);
            if (intern == null || intern.IsDelete == true || intern.IsActive == false)
                return new GetDetailEmailUserStatusResponse() { Errors = "Intern is not found" };

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return new GetDetailEmailUserStatusResponse() { Errors = "Cannot get Id from JWT token" };
            var userId = userIdClaim.Value;

            status.IdNguoiGui = userId;
            status.CreatedBy = userId;
            status.LastUpdatedBy = userId;
            status.CreatedTime = DateTimeOffset.Now;
            status.LastUpdatedTime = DateTimeOffset.Now;
            status.IsActive = true;
            status.IsDelete = false;
            await _unitOfWork.EmailUserStatusRepository.AddAsync(status);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<GetDetailEmailUserStatusResponse>(status);
        }
    }
}
