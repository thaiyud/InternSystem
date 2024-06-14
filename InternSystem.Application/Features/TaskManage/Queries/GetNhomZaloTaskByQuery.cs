using InternSystem.Application.Features.TaskManage.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Queries
{
    public class GetNhomZaloTaskByQuery : IRequest<IEnumerable<NhomZaloTaskReponse>>
    {
    }
}
