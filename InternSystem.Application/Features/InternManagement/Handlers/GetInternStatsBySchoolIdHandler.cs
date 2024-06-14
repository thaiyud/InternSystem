using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.InternManagement.Handlers
{
    public class GetInternStatsBySchoolIdHandler : IRequestHandler<GetInternStatsBySchoolIdQuery, List<GetInternStatsBySchoolIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInternStatsBySchoolIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetInternStatsBySchoolIdResponse>> Handle(GetInternStatsBySchoolIdQuery request, CancellationToken cancellationToken)
        {

            return await _unitOfWork.InternInfoRepository.GetInternStatsBySchoolIdAsync(request.SchoolId);
        }
    }
}
