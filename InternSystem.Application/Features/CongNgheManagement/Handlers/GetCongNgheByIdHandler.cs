using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Handlers
{
    public class GetCongNgheByIdHandler : IRequestHandler<GetCongNgheByIdQuery, GetCongNgheByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCongNgheByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCongNgheByIdResponse> Handle(GetCongNgheByIdQuery request, CancellationToken cancellationToken)
        {
            CongNghe? existingCN = await _unitOfWork.CongNgheRepository.GetByIdAsync(request.Id);
            if (existingCN == null || existingCN.IsDelete == true)
                return new GetCongNgheByIdResponse() { Errors = "CongNghe not found" };

            return _mapper.Map<GetCongNgheByIdResponse>(existingCN);
        }
    }
}
