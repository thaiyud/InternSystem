using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Message.Models;
using InternSystem.Application.Features.Message.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Message.Handlers
{
    public class GetMessageHistoryHandler : IRequestHandler<GetMessageHistoryQuery, List<GetMessageHistoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMessageHistoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetMessageHistoryResponse>> Handle(GetMessageHistoryQuery request, CancellationToken cancellationToken)
        {
            var messages = await _unitOfWork.MessageRepository
                .GetMessagesAsync(request.IdSender, request.IdReceiver);

            var response = messages.Select(m => new GetMessageHistoryResponse
            {
                IdSender = m.IdSender,
                IdReceiver = m.IdReceiver,
                MessageText = m.MessageText,
                Timestamp = m.Timestamp
            }).ToList();

            return response;
        }
    }
}
