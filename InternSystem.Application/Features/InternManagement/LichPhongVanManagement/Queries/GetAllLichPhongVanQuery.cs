using FluentValidation;
using InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Queries
{
    public class GetAllLichPhongVanValidator : AbstractValidator<GetAllLichPhongVanQuery>
    {
        public GetAllLichPhongVanValidator()
        {

        }
    }

    public class GetAllLichPhongVanQuery : IRequest<IEnumerable<GetAllLichPhongVanResponse>>
    {

    }
}
