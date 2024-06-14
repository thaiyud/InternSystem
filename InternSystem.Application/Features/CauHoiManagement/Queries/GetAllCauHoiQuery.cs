using InternSystem.Application.Features.CauHoiManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Queries
{
    public class GetAllCauHoiQuery : IRequest<IEnumerable<GetAllCauHoiResponse>>
    {
    }
}
