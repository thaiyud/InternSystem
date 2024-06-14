using FluentValidation;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.DashboardManage.Models;
using InternSystem.Application.Features.DuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DashboardManage
{

    public class GetAllDashboardQuery : IRequest<GetAllDashboardResponse>
    {
    }
}  
