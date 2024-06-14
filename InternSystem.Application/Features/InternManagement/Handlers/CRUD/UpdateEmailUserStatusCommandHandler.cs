using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Commands;
using InternSystem.Application.Features.InternManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.Handlers.CRUD
{
    public class UpdateEmailUserStatusCommandHandler : IRequestHandler<UpdateEmailUserStatusCommand, GetDetailEmailUserStatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateEmailUserStatusCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetDetailEmailUserStatusResponse> Handle(UpdateEmailUserStatusCommand request, CancellationToken cancellationToken)
        {
            var existStatus = await _unitOfWork.EmailUserStatusRepository.GetByIdAsync(request.Id);

            if (existStatus == null || existStatus.IsDelete || !existStatus.IsActive)
            {
                return new GetDetailEmailUserStatusResponse() { Errors = "Email user status is not found" };
            }

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return new GetDetailEmailUserStatusResponse() { Errors = "Cannot get Id from JWT token" };
            var userId = userIdClaim.Value;

            _mapper.Map(request, existStatus);
            existStatus.LastUpdatedTime = DateTimeOffset.Now;
            existStatus.LastUpdatedBy = userId;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<GetDetailEmailUserStatusResponse>(existStatus);
        }
    }
}
