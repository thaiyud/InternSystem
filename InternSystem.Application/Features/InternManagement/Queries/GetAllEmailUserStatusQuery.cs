using InternSystem.Application.Features.InternManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.Queries
{
    public class GetAllEmailUserStatusQuery: IRequest<IEnumerable<GetDetailEmailUserStatusResponse>>
    {
    }
}
