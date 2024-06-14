using FluentValidation;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Commands
{
    public class CreateCongNgheDuAnValidator : AbstractValidator<CreateCongNgheDuAnCommand>
    {
        public CreateCongNgheDuAnValidator()
        {
            RuleFor(m => m.IdCongNghe).NotEmpty();
        }
    }
    public class CreateCongNgheDuAnCommand : IRequest<CreateCongNgheDuAnResponse>
    {
        public int IdCongNghe { get; set; }
        public int IdDuAn { get; set; }

        [JsonIgnore]
        public string? CreatedBy { get; set; }

    }
}
