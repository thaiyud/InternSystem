using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.User.Queries
{
    public class GetTruongHocByTenQuery : IRequest<IEnumerable<TruongHoc>>
    {
        public string Ten { get; set; }

        public GetTruongHocByTenQuery(string ten)
        {
            Ten = ten;
        }
    }
}
