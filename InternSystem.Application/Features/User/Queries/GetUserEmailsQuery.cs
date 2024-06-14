using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Queries
{
    public class GetUserEmailsQuery : IRequest<IEnumerable<string>>
    {
    }
}
