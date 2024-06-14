using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Auth.Models;
using InternSystem.Application.Features.Auth.Queries;
using MediatR;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class GetEmailsWithIndicesQueryHandler : IRequestHandler<GetEmailsWithIndicesQuery, IEnumerable<EmailWithIndexResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmailsWithIndicesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmailWithIndexResponse>> Handle(GetEmailsWithIndicesQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.InternInfoRepository.GetAllASync();
            var emailsWithIndices = users.Select((user, index) => new EmailWithIndexResponse { Index = index, Email = user.EmailCaNhan });
            return emailsWithIndices;
        }
    }
}
