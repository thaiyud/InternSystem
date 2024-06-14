using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.UserDuAnManagement.Models;
using InternSystem.Application.Features.UserDuAnManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.UserDuAnManagement.Handlers
{
    public class GetUserDuAnByIdHandler : IRequestHandler<GetUserDuAnByIdQuery, GetUserDuAnByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserDuAnByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserDuAnByIdResponse> Handle(GetUserDuAnByIdQuery request, CancellationToken cancellationToken)
        {
            UserDuAn? existingUserDA = await _unitOfWork.UserDuAnRepository.GetByIdAsync(request.Id);
            if (existingUserDA == null || existingUserDA.IsDelete == true)
                return new GetUserDuAnByIdResponse() { Errors = "User DuAn not found" };

            return _mapper.Map<GetUserDuAnByIdResponse>(existingUserDA);
        }
    }
}
