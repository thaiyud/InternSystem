using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.UserViTriManagement.Commands;
using InternSystem.Application.Features.UserViTriManagement.Models;
using InternSystem.Application.Features.ViTriManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserViTriManagement.Handlers.CRUD
{
    public class UpdateUserViTriHandler : IRequestHandler<UpdateUserViTriCommand, UpdateUserViTriResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserViTriHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateUserViTriResponse> Handle(UpdateUserViTriCommand request, CancellationToken cancellationToken)
        {
            UserViTri? existingUserViTri = await _unitOfWork.UserViTriRepository.GetByIdAsync(request.Id);
            if (existingUserViTri == null || existingUserViTri.IsDelete == true) return new UpdateUserViTriResponse() { Errors = "User Vi Tri not found" };


            if (!(request.UserId.IsNullOrEmpty()))
            {
                AspNetUser existingUser = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId!);
                if (existingUser == null) return new UpdateUserViTriResponse() { Errors = "User not found" };
            }
            if (!(request.IdViTri >0))
            {
                ViTri existingViTri = await _unitOfWork.ViTriRepository.GetByIdAsync(request.IdViTri!);
                if (existingViTri == null) return new UpdateUserViTriResponse() { Errors = "ViTri not found" };
            }

            existingUserViTri = _mapper.Map(request, existingUserViTri);

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateUserViTriResponse>(existingUserViTri);
        }
    }
}
