using FluentValidation;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.CuocPhongVanManagement.Queries
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

