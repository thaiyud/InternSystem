using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.PhongVanManagement.Commands;
using InternSystem.Application.Features.PhongVanManagement.Models;
using InternSystem.Application.Features.PhongVanManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.PhongVanManagement.Handlers
{
    public class GetAllPhongVanHandler : IRequestHandler<GetAllPhongVanQuery, IEnumerable<PhongVan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhongVan>> Handle(GetAllPhongVanQuery request, CancellationToken cancellationToken)
        {
            var PhongVanList = await _unitOfWork.PhongVanRepository.GetAllPhongVan();
            return PhongVanList;
        }

    }
}
