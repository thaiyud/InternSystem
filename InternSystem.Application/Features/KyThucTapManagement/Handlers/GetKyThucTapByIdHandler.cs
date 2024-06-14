using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using InternSystem.Application.Features.KyThucTapManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.KyThucTapManagement.Handlers
{
    public class GetKyThucTapByIdHandler : IRequestHandler<GetKyThucTapByIdQuery, GetKyThucTapByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetKyThucTapByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetKyThucTapByIdResponse> Handle(GetKyThucTapByIdQuery request, CancellationToken cancellationToken)
        {
            KyThucTap? result = await _unitOfWork.KyThucTapRepository.GetByIdAsync(request.Id);

            if (result == null || result.IsDelete) 
                return new GetKyThucTapByIdResponse() { Errors = "KyThucTap not found" };

            return _mapper.Map<GetKyThucTapByIdResponse>(result);
        }
    }
}
