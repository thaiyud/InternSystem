using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSystem.Application.Features.User.Commands
{
    public class AddUserToRoleCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }

        public AddUserToRoleCommand(string userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}

