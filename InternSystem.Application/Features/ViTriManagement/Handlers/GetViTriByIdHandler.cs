using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ViTriManagement.Models;
using InternSystem.Application.Features.ViTriManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ViTriManagement.Handlers
{
    public class GetUserViTriByIdHandler : IRequestHandler<GetViTriByIdQuery, GetViTriByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserViTriByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetViTriByIdResponse> Handle(GetViTriByIdQuery request, CancellationToken cancellationToken)
        {
            ViTri? existingViTri = await _unitOfWork.ViTriRepository.GetByIdAsync(request.Id);
            if (existingViTri == null || existingViTri.IsDelete == true)
                return new GetViTriByIdResponse() { Errors = "ViTri not found" };

            return _mapper.Map<GetViTriByIdResponse>(existingViTri);
        }
    }
}
