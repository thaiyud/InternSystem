using InternSystem.Application.Features.User.Models.UserModels;
using MediatR;

namespace InternSystem.Application.Features.User.Queries
{
    public class GetAllUserQuery : IRequest<IEnumerable<GetAllUserResponse>>
    {
    }
}
