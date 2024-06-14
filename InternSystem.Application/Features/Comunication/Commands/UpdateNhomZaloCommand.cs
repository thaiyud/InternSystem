using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Comunication.Commands
{
    public class UpdateNhomZaloCommand : IRequest<UpdateNhomZaloResponse>
    {
        public string TenNhom { get; set; }
        public string LinkNhom { get; set; }
        public bool IsNhomChung { get; set; }

    }
}
