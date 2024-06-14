using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using InternSystem.Application.Features.CongNgheDuAnManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheDuAnManagement.Handlers
{
    public class GetCongNgheDuAnByIdHandler : IRequestHandler<GetCongNgheDuAnByIdQuery, GetCongNgheDuAnByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCongNgheDuAnByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCongNgheDuAnByIdResponse> Handle(GetCongNgheDuAnByIdQuery request, CancellationToken cancellationToken)
        {
            CongNgheDuAn? existingCNDA = await _unitOfWork.CongNgheDuAnRepository.GetByIdAsync(request.Id);
            if (existingCNDA == null || existingCNDA.IsDelete == true)
                return new GetCongNgheDuAnByIdResponse() { Errors = "CongNgheDuAn not found" };

            return _mapper.Map<GetCongNgheDuAnByIdResponse>(existingCNDA);
        }
    }
}
