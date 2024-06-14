using InternSystem.Application.Features.CongNgheManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Queries
{
    public class GetAllCongNgheQuery : IRequest<IEnumerable<GetAllCongNgheResponse>>
    {
    }
}
