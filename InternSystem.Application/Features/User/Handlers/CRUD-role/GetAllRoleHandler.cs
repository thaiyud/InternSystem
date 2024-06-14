using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetAllRoleHandler : IRequestHandler<GetRoleQuery, IEnumerable<GetRoleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public GetAllRoleHandler(IUnitOfWork unitOfWork, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetRoleResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            // Get all roles using RoleManager
            var roles = await _roleManager.Roles.ToListAsync();

            // Map roles to GetRoleResponse objects
            return _mapper.Map<IEnumerable<GetRoleResponse>>(roles);
        }
    }
}
