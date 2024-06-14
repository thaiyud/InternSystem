using InternSystem.Application.Features.Interview.Models;
using MediatR;

namespace InternSystem.Application.Features.Interview.Queries
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<GetDetailCommentResponse>>
    {
    }
}
