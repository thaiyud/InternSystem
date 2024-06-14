using FluentValidation;
using InternSystem.Application.Features.PhongVanManagement.Models;
using MediatR;

namespace InternSystem.Application.Features.PhongVanManagement.Commands
{
    public class UpdatePhongVanValidator : AbstractValidator<UpdatePhongVanCommand>
    {
        public UpdatePhongVanValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }
    public class UpdatePhongVanCommand : IRequest<UpdatePhongVanResponse>
    {
        public int Id { get; set; }
        public string? CauTraLoi { get; set; }
        public decimal? Rank { get; set; }
        public string? NguoiCham { get; set; }
        public DateTime? RankDate { get; set; }
        public int? IdCauHoiCongNghe { get; set; }
        public int? IdLichPhongVan { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
