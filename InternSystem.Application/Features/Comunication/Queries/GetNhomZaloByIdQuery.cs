using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Comunication.Queries
{
    public class GetNhomZaloByIdQuery : IRequest<GetNhomZaloResponse>
    {
        public int Id { get; set; }

        public GetNhomZaloByIdQuery(int id)
        {
            Id = id;
        }
    }
}
