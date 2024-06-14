using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Commands.User;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace InternSystem.Application.Features.User.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == request.Username))
                throw new DuplicateNameException($"Username {request.Username} already exists");

            var roleName = request.RoleName.Trim();
            IdentityRole? role = await _roleManager.FindByNameAsync(roleName);

            if (role == null) 
                throw new ArgumentNullException(nameof(request.RoleName), "Role not found");

            if (request.InternInfoId != null)
            {
                if (await _unitOfWork.InternInfoRepository.GetByIdAsync(request.InternInfoId) == null)
                    throw new ArgumentNullException(nameof(request.InternInfoId), "Intern information not found");
            }
       
            AspNetUser newUser = _mapper.Map<AspNetUser>(request);

            IdentityResult resultCreateUser = await _userManager.CreateAsync(newUser, request.Password);

            if (!resultCreateUser.Succeeded) throw new InvalidOperationException(resultCreateUser.Errors.FirstOrDefault()?.Description);

            IdentityResult resultAddRole = await _userManager.AddToRoleAsync(newUser, role.Name!);

            if (!resultAddRole.Succeeded) throw new InvalidOperationException(resultAddRole.Errors.FirstOrDefault()?.Description);

            return _mapper.Map<CreateUserResponse>(newUser);
        }
    }
}
