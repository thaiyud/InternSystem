using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.InternManagement.Handlers
{
    public class GetInternInfoQueryHandler : IRequestHandler<GetInternInfoQuery, GetInternInfoResponse<InternInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInternInfoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetInternInfoResponse<InternInfo>> Handle(GetInternInfoQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.InternInfoRepository.GetInternInfosAsync(request);
        }
    }
}
