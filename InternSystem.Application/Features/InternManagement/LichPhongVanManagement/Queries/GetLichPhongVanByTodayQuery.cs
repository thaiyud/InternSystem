using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Queries
{
    public class GetLichPhongVanByTodayQuery : IRequest<IEnumerable<LichPhongVan>>
    {

        public GetLichPhongVanByTodayQuery()
        {
        }
    }

}

