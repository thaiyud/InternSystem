using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Comunication.Commands
{
    public class UpdateUserNhomZaloCommand : IRequest<UpdateUserNhomZaloResponse>
    {
        public bool isMentor { get; set; }
        public bool IsLeader { get; set; }
    }
}
