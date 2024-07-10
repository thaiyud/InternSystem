using MediatR;

namespace InternSystem.Application.Features.AuthManagement.UserRoleManagement.Queries
{
    public class GetAspNetUserRoleByRoleIdQuery : IRequest<List<string>>
    {
        public string RoleId { get; set; }
    }
}
