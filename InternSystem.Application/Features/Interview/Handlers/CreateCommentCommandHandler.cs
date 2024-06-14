using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Interview.Commands;
using InternSystem.Application.Features.Interview.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, GetDetailCommentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCommentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetDetailCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request);

            var intern = await _unitOfWork.InternInfoRepository.GetByIdAsync(request.IdNguoiDuocComment);
            if (intern == null || intern.IsDelete == true || intern.IsActive == false)
                return new GetDetailCommentResponse() { Errors = "Intern is not found" };

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "Id");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return new GetDetailCommentResponse() { Errors = "Cannot get Id from JWT token" };
            var userId = userIdClaim.Value;

            comment.IdNguoiComment = userId;
            comment.CreatedBy = userId;
            comment.LastUpdatedBy = userId;
            comment.CreatedTime = DateTimeOffset.Now;
            comment.LastUpdatedTime = DateTimeOffset.Now;
            comment.IsActive = true;
            comment.IsDelete = false;
            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<GetDetailCommentResponse>(comment);
        }
    }
}
