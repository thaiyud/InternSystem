using InternSystem.Application.Features.Message.Models;
using MediatR;
using System.Collections.Generic;

namespace InternSystem.Application.Features.Message.Queries
{
    public class GetMessageHistoryQuery : IRequest<List<GetMessageHistoryResponse>>
    {
        public string IdSender { get; set; }
        public string IdReceiver { get; set; }
    }
}
