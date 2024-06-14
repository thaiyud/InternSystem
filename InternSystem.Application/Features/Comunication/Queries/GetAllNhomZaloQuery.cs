using InternSystem.Application.Features.Comunication.Models;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Comunication.Queries
{
    public class GetAllNhomZaloQuery : IRequest<IEnumerable<GetNhomZaloResponse>>
    {
    }
}
