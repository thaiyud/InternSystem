using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Application.Features.LichPhongVanManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.LichPhongVanManagement.Handlers
{
    public class GetLichPhongvanByIdHandler : IRequestHandler<GetLichPhongVanByIdQuery, GetLichPhongVanByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLichPhongvanByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetLichPhongVanByIdResponse> Handle(GetLichPhongVanByIdQuery request, CancellationToken cancellationToken)
        {
            LichPhongVan? existingDA = await _unitOfWork.LichPhongVanRepository.GetByIdAsync(request.Id);
            if (existingDA == null || existingDA.IsDelete == true)
                return new GetLichPhongVanByIdResponse { Errors = "Lich phong van not found" };

            return _mapper.Map<GetLichPhongVanByIdResponse>(existingDA);
        }
    }
}
