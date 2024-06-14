using InternSystem.Application.Features.User.Models;
using MediatR;

namespace InternSystem.Application.Features.User.Queries
{
    public class GetRoleQuery : IRequest<IEnumerable<GetRoleResponse>>
    {
    }
}
