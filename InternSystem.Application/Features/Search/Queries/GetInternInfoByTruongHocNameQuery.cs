using InternSystem.Application.Features.Search.Models;
using MediatR;

namespace InternSystem.Application.Features.Search.Queries
{
    public class GetInternInfoByTruongHocNameQuery : IRequest<IEnumerable<GetInternInfoResponse>>
    {
        public string TruongHocName { get; set; }

        public GetInternInfoByTruongHocNameQuery(string truongHocName)
        {
            TruongHocName = truongHocName;
        }
    }
}
