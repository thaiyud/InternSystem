using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Interview.Models;
using InternSystem.Application.Features.Interview.Queries;
using MediatR;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<GetDetailCommentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDetailCommentResponse>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync();
            var filteredComments = comments.Where(c => c.IsActive && !c.IsDelete);
            return _mapper.Map<IEnumerable<GetDetailCommentResponse>>(filteredComments);
        }
    }
}
