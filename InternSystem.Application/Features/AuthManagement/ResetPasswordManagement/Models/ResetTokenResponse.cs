﻿namespace InternSystem.Application.Features.AuthManagement.ResetPasswordManagement.Models
{
    public class ResetTokenResponse
    {
        public string VerificationToken { get; set; }
        public string ResetToken { get; set; }
    }
}
