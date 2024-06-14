using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Interview.Models;
using InternSystem.Application.Features.Interview.Queries;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, GetDetailCommentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetDetailCommentResponse> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            Comment? existComment = await _unitOfWork.CommentRepository.GetByIdAsync(request.Id);
            if (existComment == null || existComment.IsDelete == true)
                return new GetDetailCommentResponse() { Errors = "Comment is not found" };

            return _mapper.Map<GetDetailCommentResponse>(existComment);
        }
    }
}
