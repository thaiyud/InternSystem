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
    public class UpdateCongNgheDuAnValidator : AbstractValidator<UpdateCongNgheDuAnCommand>
    {
        public UpdateCongNgheDuAnValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
        }
    }


    public class UpdateCongNgheDuAnCommand : IRequest<UpdateCongNgheDuAnResponse>
    {
        public int Id { get; set; }
        public int IdCongNghe { get; set; }
        public int IdDuAn { get; set; }
        [JsonIgnore]
        public string? LastUpdatedBy { get; set; }
    }
}
