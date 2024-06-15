using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Application.Features.User.Queries;
using MediatR;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<GetAllUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllUserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetAllAsync();
            Console.WriteLine(user);
            return _mapper.Map<IEnumerable<GetAllUserResponse>>(user);
        }
    }
}
