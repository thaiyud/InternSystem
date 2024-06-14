using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.UserViTriManagement.Models;
using InternSystem.Application.Features.UserViTriManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserViTriManagement.Handlers
{
    public class GetUserViTriByIdHandler : IRequestHandler<GetUserViTriByIdQuery, GetUserViTriByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserViTriByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserViTriByIdResponse> Handle(GetUserViTriByIdQuery request, CancellationToken cancellationToken)
        {
            UserViTri? existingUserViTri = await _unitOfWork.UserViTriRepository.GetByIdAsync(request.Id);
            if (existingUserViTri == null || existingUserViTri.IsDelete == true)
                return new GetUserViTriByIdResponse() { Errors = "UserViTri not found" };

            return _mapper.Map<GetUserViTriByIdResponse>(existingUserViTri);
        }
    }
}
