
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Queries
{
    public class GetFilteredInternInfoByDayQuery : IRequest<IEnumerable<InternInfo>>
    {
        public DateTime? Day { get; set; }
    }

}
