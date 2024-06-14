using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Queries
{
    public class GetAllCongNgheDuAnQuery : IRequest<IEnumerable<GetAllCongNgheDuAnResponse>>
    {
    }
}
