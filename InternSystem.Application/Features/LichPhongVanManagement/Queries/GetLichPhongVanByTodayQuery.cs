using FluentValidation;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Queries
{
    public class GetLichPhongVanByTodayQuery : IRequest<IEnumerable<LichPhongVan>>
    {

        public GetLichPhongVanByTodayQuery()
        {
        }
    }

}

