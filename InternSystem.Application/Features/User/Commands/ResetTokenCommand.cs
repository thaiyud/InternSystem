using InternSystem.Application.Features.User.Models.ResetTokenModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Commands
{
    public class ResetTokenCommand : IRequest<ResetTokenResponse>
    {
        public ResetTokenRequest ResetTokenRequest { get; }
        public ResetTokenCommand(ResetTokenRequest resetTokenRequest)
        {
            ResetTokenRequest = resetTokenRequest;
        }
    }
}
