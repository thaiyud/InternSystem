﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Auth.Models
{
    public class SendEmailsRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailType { get; set; }
    }
}
