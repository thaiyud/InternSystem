using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Models;
using InternSystem.Application.Features.Comunication.Queries;
using InternSystem.Application.Features.User.Models.UserModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetUserNhomZaloByIdQueryHandler : IRequestHandler<GetUserNhomZaloByIdQuery, GetUserNhomZaloResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserNhomZaloByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserNhomZaloResponse> Handle(GetUserNhomZaloByIdQuery request, CancellationToken cancellationToken)
        {
            var userNhomZalo = await _unitOfWork.UserNhomZaloRepository.GetByIdAsync(request.Id);

            if (userNhomZalo == null)
            {
                return null;
            }

            return _mapper.Map<GetUserNhomZaloResponse>(userNhomZalo);
        }
    }
}
