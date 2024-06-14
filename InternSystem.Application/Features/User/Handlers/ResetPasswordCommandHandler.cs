using InternSystem.Application.Common.EmailService;
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
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IdentityResult>
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IEmailService _emailService;

        public ResetPasswordCommandHandler(UserManager<AspNetUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IdentityResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid request." });
            }

            /*
             * var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.EmailCode, request.Code);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid token." });
            }
            */

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);
            if (result.Succeeded)
            {
                //user.EmailCode = null; 
                await _userManager.UpdateAsync(user);
                var selectedEmail = new List<string> { request.Email };
                var time = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                await _emailService.SendEmailAsync(selectedEmail, "Your password has changed", $"Your password has changed successfully at: {time}");
                return IdentityResult.Success;
            }

            return result;
        }
    }

}
