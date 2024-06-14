using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiCongNgheManagement.Queries
{
    public class GetAllCauHoiCongNgheQuery : IRequest<IEnumerable<GetAllCauHoiCongNgheResponse>>
    {

    }
}
