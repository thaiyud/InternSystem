using FluentValidation;
using InternSystem.Application.Features.PhongVanManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.PhongVanManagement.Queries
{
    public class GetAllPhongVanValidator : AbstractValidator<GetAllPhongVanQuery>
    {
        public GetAllPhongVanValidator()
        {

        }
    }

    public class GetAllPhongVanQuery : IRequest<IEnumerable<PhongVan>>
    {

    }
}

