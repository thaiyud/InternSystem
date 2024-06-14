using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Comunication.Commands
{
    public class DeleteUserNhomZaloCommand : IRequest<DeleteUserNhomZaloResponse>
    {
        public int Id { get; set; }
    }

}
