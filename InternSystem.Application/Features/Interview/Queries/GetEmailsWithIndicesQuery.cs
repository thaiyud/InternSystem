using InternSystem.Application.Features.Auth.Models;
using MediatR;

namespace InternSystem.Application.Features.Auth.Queries
{
    public class GetEmailsWithIndicesQuery : IRequest<IEnumerable<EmailWithIndexResponse>>
    {
    }
}
