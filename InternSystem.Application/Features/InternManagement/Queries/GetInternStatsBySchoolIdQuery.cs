using InternSystem.Application.Features.InternManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.InternManagement.Queries
{
    public class GetInternStatsBySchoolIdQuery : IRequest<List<GetInternStatsBySchoolIdResponse>>
    {
        public int SchoolId { get; set; }
    }
}
