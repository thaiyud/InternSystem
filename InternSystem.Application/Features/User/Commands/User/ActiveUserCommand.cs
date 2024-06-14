using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Commands.User
{
    public class ActiveUserCommand :IRequest<bool>
    {
        public required string UserId { get; set; }
        public bool IsActive { get; set; }
        public ActiveUserCommand(string userId, bool isActive)
        {
            UserId = userId;
            IsActive = isActive;
        }
    }
}
