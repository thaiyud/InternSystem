using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.InternManagement.Queries
{
    public class GetFilteredInternInfoByDayQuery : IRequest<IEnumerable<InternInfo>>
    {
        public DateTime? Day { get; set; }
    }

}
