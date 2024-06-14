using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Models;
using InternSystem.Application.Features.Comunication.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetNhomZaloByIdQueryHandler : IRequestHandler<GetNhomZaloByIdQuery, GetNhomZaloResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNhomZaloByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetNhomZaloResponse> Handle(GetNhomZaloByIdQuery request, CancellationToken cancellationToken)
        {
            var nhomZalo = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.Id);

            if (nhomZalo == null)
            {
           
                return null; 
            }

            return _mapper.Map<GetNhomZaloResponse>(nhomZalo);
        }
    }

}
