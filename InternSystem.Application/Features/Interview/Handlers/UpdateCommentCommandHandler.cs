using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Interview.Commands;
using InternSystem.Application.Features.Interview.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, GetDetailCommentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCommentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetDetailCommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? existComment = await _unitOfWork.CommentRepository.GetByIdAsync(request.Id);

            if (existComment == null || existComment.IsDelete || !existComment.IsActive)
            {
                return new GetDetailCommentResponse() { Errors = "Comment is not found" };
            }

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return new GetDetailCommentResponse() { Errors = "Cannot get Id from JWT token" };
            var userId = userIdClaim.Value;

            _mapper.Map(request, existComment);
            existComment.LastUpdatedTime = DateTimeOffset.Now;
            existComment.LastUpdatedBy = userId;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<GetDetailCommentResponse>(existComment);
        }
    }
}
