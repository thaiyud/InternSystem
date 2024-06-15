using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSystem.Application.Features.BaseFeature.Queries
{
    public class GetAllQuery<TResponse> : IRequest<IEnumerable<TResponse>>
    {
    }
}
