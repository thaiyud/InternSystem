using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetUsersByHoVaTenQueryHandler : IRequestHandler<GetUsersByHoVaTenQuery, IEnumerable<AspNetUser>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersByHoVaTenQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AspNetUser>> Handle(GetUsersByHoVaTenQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUsersByHoVaTenAsync(request.HoVaTen);
            Console.WriteLine(user);
            return _mapper.Map<IEnumerable<GetAllUserResponse>>(user);
        }
    }
}
