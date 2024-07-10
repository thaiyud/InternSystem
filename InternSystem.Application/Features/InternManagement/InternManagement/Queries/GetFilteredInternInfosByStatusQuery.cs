using InternSystem.Application.Features.InternManagement.InternManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.InternManagement.Queries
{
    public class GetFilteredInternInfosByStatusQuery : IRequest<IEnumerable<GetInternInfoResponse>>
    {
        public string? TrangThai { get; set; }

        public GetFilteredInternInfosByStatusQuery(string trangThai)
        {
            TrangThai = trangThai;
        }
    }
}
