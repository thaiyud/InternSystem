using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Comunication.Commands
{
    public class AddUserToNhomZaloCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int NhomZaloId { get; set; }
        public bool IsMentor { get; set; }
        public bool IsLeader { get; set; }
    }
}
