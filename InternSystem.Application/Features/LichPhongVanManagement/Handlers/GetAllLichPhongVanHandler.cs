using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.LichPhongVanManagement.Commands;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Application.Features.LichPhongVanManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Handlers
{
    public class GetAllLichPhongVanHandler : IRequestHandler<GetAllLichPhongVanQuery, IEnumerable<LichPhongVan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLichPhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LichPhongVan>> Handle(GetAllLichPhongVanQuery request, CancellationToken cancellationToken)
        {
            var lichPhongVanList = await _unitOfWork.LichPhongVanRepository.GetAllLichPhongVan();
            return lichPhongVanList;
        }

    }
}
