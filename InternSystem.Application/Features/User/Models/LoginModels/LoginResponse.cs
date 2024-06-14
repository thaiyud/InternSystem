using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Models.LoginModels
{
    public class LoginResponse
    {
        public string VerificationToken { get; set; }
        public string ResetToken { get; set; }
    }
}
