using InternSystem.Application.Features.User.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class CheckValidCodeCommandHandler : IRequestHandler<VerifyCodeCommand, bool>
    {
        private readonly UserManager<AspNetUser> _userManager;

        public CheckValidCodeCommandHandler(UserManager<AspNetUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return false;
            }

            var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.EmailCode, request.Code);
            return verificationResult != PasswordVerificationResult.Failed;
        }
    }
}
