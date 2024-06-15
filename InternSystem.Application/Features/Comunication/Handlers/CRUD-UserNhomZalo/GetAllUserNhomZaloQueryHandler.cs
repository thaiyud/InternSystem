using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Models;
using InternSystem.Application.Features.Comunication.Queries;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetAllUserNhomZaloQueryHandler : IRequestHandler<GetAllUserNhomZaloQuery, IEnumerable<GetUserNhomZaloResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserNhomZaloQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUserNhomZaloResponse>> Handle(GetAllUserNhomZaloQuery request, CancellationToken cancellationToken)
        {
            var userNhomZaloEntities = await _unitOfWork.UserNhomZaloRepository.GetAllAsync();
            var userNhomZaloResponses = _mapper.Map<IEnumerable<GetUserNhomZaloResponse>>(userNhomZaloEntities);
            return userNhomZaloResponses;
        }
    }
}
