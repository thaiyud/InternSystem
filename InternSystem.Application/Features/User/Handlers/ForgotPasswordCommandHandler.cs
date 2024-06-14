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
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(UserManager<AspNetUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unit.Value;
            }

            var code = new Random().Next(1000, 9999).ToString();
            var codeHash = _userManager.PasswordHasher.HashPassword(user, code);

            user.EmailCode = codeHash;
            await _userManager.UpdateAsync(user);

            var selectedEmail = new List<string> { request.Email };
            await _emailService.SendEmailAsync(selectedEmail, "Password Reset Code", $"Your reset code is: {code}");

            return Unit.Value;
        }
    }
}
