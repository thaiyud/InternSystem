using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.DuAnManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DuAnManagement.Handlers
{
    public class GetDuAnByIdHandler : IRequestHandler<GetDuAnByIdQuery, GetDuAnByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDuAnByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetDuAnByIdResponse> Handle(GetDuAnByIdQuery request, CancellationToken cancellationToken)
        {
            DuAn? existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.Id);
            if (existingDA == null || existingDA.IsDelete == true) 
                return new GetDuAnByIdResponse() { Errors = "DuAn not found" };

            return _mapper.Map<GetDuAnByIdResponse>(existingDA);
        }
    }
}
