using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Application.Features.LichPhongVanManagement.Queries;
using InternSystem.Application.Features.PhongVanManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.PhongVanManagement.Handlers
{
    public class GetPhongVanByIdHandler : IRequestHandler<GetPhongVanByIdQuery, GetPhongVanByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPhongVanByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetPhongVanByIdResponse> Handle(GetPhongVanByIdQuery request, CancellationToken cancellationToken)
        {
            PhongVan? existingDA = await _unitOfWork.PhongVanRepository.GetByIdAsync(request.Id);
            if (existingDA == null || existingDA.IsDelete == true)
                return new GetPhongVanByIdResponse { Errors = "Phong van not found" };

            return _mapper.Map<GetPhongVanByIdResponse>(existingDA);
        }
    }
}
