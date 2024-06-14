using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.User.Queries
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
