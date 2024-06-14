using FluentValidation;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Queries
{
    public class GetAllThongBaoValidator : AbstractValidator<GetAllThongBaoQuery>
    {
        public GetAllThongBaoValidator()
        {
            
        }
    }

    public class GetAllThongBaoQuery : IRequest<GetAllThongBaoResponse>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
