using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Models;
using InternSystem.Application.Features.Comunication.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetAllNhomZaloQueryHandler : IRequestHandler<GetAllNhomZaloQuery, IEnumerable<GetNhomZaloResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllNhomZaloQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetNhomZaloResponse>> Handle(GetAllNhomZaloQuery request, CancellationToken cancellationToken)
        {
            var nhomZalos = await _unitOfWork.NhomZaloRepository.GetAllASync();

            if (nhomZalos == null || !nhomZalos.Any())
            {

                return new List<GetNhomZaloResponse>(); 
            }

            return _mapper.Map<IEnumerable<GetNhomZaloResponse>>(nhomZalos);
        }
    }
}
