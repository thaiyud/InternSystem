using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Interview.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? existComment = await _unitOfWork.CommentRepository.GetByIdAsync(request.Id);
            if (existComment == null || existComment.IsDelete == true)
                return false;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return false;
            var userId = userIdClaim.Value;

            existComment.DeletedBy = userId;
            existComment.DeletedTime = DateTimeOffset.Now;
            existComment.IsActive = false;
            existComment.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
