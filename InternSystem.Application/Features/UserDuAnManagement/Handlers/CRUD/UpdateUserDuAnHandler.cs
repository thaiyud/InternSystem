using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.UserDuAnManagement.Commands;
using InternSystem.Application.Features.UserDuAnManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserDuAnManagement.Handlers.CRUD
{
    public class UpdateUserDuAnHandler : IRequestHandler<UpdateUserDuAnCommand, UpdateUserDuAnResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateUserDuAnResponse> Handle(UpdateUserDuAnCommand request, CancellationToken cancellationToken)
        {
            UserDuAn? existingUserDA = await _unitOfWork.UserDuAnRepository.GetByIdAsync(request.Id);
            if (existingUserDA == null || existingUserDA.IsDelete == true) return new UpdateUserDuAnResponse() { Errors = "DuAn not found" };

            if (!request.UserId.IsNullOrEmpty())
            {
                AspNetUser? leader = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId!);
                if (leader == null) return new UpdateUserDuAnResponse() { Errors = "User not found" };
            }
            if (!(request.DuAnId < 0))
            {
                DuAn existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId!);
                if (existingDA == null) return new UpdateUserDuAnResponse() { Errors = "Du An not found" };
            }

            existingUserDA = _mapper.Map(request, existingUserDA);

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateUserDuAnResponse>(existingUserDA);
        }
    }
}
