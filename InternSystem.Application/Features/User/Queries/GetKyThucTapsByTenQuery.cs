using InternSystem.Application.Features.User.Models;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.User.Queries
{
    public class GetKyThucTapsByTenQuery : IRequest<IEnumerable<GetKyThucTapsByNameResponse>>
    {
        public string Ten { get; set; }

        public GetKyThucTapsByTenQuery(string ten)
        {
            Ten = ten;
        }
    }
}

