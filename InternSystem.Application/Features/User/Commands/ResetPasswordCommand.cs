using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Commands
{
    public class ResetPasswordCommand : IRequest<IdentityResult>
    {
        public string Email { get; set; }
        //public string Code { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public ResetPasswordCommand(string email, string newPassword, string confirmPassword)
        {
            Email = email;
            //Code = code;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }
    }
}
