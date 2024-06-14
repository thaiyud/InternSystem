using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.Comunication.Models;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class CreateNhomZaloCommandHandler : IRequestHandler<CreateNhomZaloCommand, CreateNhomZaloResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateNhomZaloCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CreateNhomZaloResponse> Handle(CreateNhomZaloCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext is null");
            }

            var user = httpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new Exception("User ID claim is not found");
            }

            var nhomZalo = new NhomZalo
            {
                TenNhom = request.TenNhom,
                LinkNhom = request.LinkNhom,
                IsNhomChung = request.IsNhomChung,
                CreatedBy = currentUserId,
                LastUpdatedBy = currentUserId,
                CreatedTime = DateTimeOffset.Now,
                LastUpdatedTime = DateTimeOffset.Now,
                IsActive = true,
                IsDelete = false
            };

            await _unitOfWork.NhomZaloRepository.AddAsync(nhomZalo);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CreateNhomZaloResponse>(nhomZalo);
        }
    }
}
