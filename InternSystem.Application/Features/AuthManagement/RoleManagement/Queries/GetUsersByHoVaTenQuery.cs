using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.AuthManagement.RoleManagement.Queries
{
    public class GetUsersByHoVaTenQuery : IRequest<IEnumerable<AspNetUser>>
    {

        public string HoVaTen { get; set; }

        public GetUsersByHoVaTenQuery(string hoVaTen)
        {
            HoVaTen = hoVaTen;
        }
    }
}
