using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Token.Models
{
    public class GenerateTokenRequest
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
