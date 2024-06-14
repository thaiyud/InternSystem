using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Interview.Commands;
using MediatR;

namespace InternSystem.Application.Features.Interview.Handlers
{
    public class SelectEmailsCommandHandler : IRequestHandler<SelectEmailsCommand, IEnumerable<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SelectEmailsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<string>> Handle(SelectEmailsCommand request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.InternInfoRepository.GetAllASync();
            var availableEmails = users.Select(user => user.EmailCaNhan).ToList();

            var selectedEmails = new List<string>();
            foreach (var index in request.Indices)
            {
                if (index >= 0 && index < availableEmails.Count)
                {
                    selectedEmails.Add(availableEmails[index]);
                }
            }

            return selectedEmails;
        }
    }
}
