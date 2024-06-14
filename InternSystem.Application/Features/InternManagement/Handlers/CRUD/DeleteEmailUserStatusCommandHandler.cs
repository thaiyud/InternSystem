using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.Handlers.CRUD
{
    public class DeleteEmailUserStatusCommandHandler : IRequestHandler<DeleteEmailUserStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteEmailUserStatusCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteEmailUserStatusCommand request, CancellationToken cancellationToken)
        {
            var existStatus = await _unitOfWork.EmailUserStatusRepository.GetByIdAsync(request.Id);
            if (existStatus == null || existStatus.IsDelete == true)
                return false;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return false;
            var userId = userIdClaim.Value;

            existStatus.DeletedBy = userId;
            existStatus.DeletedTime = DateTimeOffset.Now;
            existStatus.IsActive = false;
            existStatus.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
