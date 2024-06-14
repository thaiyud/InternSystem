using MediatR;
using FluentValidation;
using InternSystem.Application.Features.PhongVanManagement.Models;

namespace InternSystem.Application.Features.PhongVanManagement.Commands
{
    public class CreatePhongVanValidator : AbstractValidator<CreatePhongVanCommand>
    {
        public CreatePhongVanValidator()
        {
            RuleFor(m => m.CauTraLoi).NotEmpty();
            RuleFor(m => m.Rank).GreaterThanOrEqualTo(0);
            RuleFor(m => m.NguoiCham).NotEmpty();
            RuleFor(m => m.RankDate).NotEmpty();
            RuleFor(m => m.IdCauHoiCongNghe).GreaterThan(0);
            RuleFor(m => m.IdLichPhongVan).GreaterThan(0);
            RuleFor(m => m.CreatedBy).NotEmpty();
        }
    }
    public class CreatePhongVanCommand : IRequest<CreatePhongVanResponse>
    {
        public string CauTraLoi { get; set; }
        public decimal Rank { get; set; }
        public string NguoiCham { get; set; }
        public DateTime RankDate { get; set; }
        public int IdCauHoiCongNghe { get; set; }
        public int IdLichPhongVan { get; set; }
        public string? CreatedBy { get; set; }
    }
}
