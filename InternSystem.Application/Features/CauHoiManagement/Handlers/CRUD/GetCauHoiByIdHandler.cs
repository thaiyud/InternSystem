using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Application.Features.CauHoiManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Handlers.CRUD
{
    public class GetCauHoiByIdHandler : IRequestHandler<GetCauHoiByIdQuery, GetCauHoiByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCauHoiByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCauHoiByIdResponse> Handle(GetCauHoiByIdQuery request, CancellationToken cancellationToken)
        {
            CauHoi? existingCauHoi = await _unitOfWork.CauHoiRepository.GetByIdAsync(request.Id);
            if (existingCauHoi == null || !existingCauHoi.IsActive)
            {
                return new GetCauHoiByIdResponse() { Errors = "Cau Hoi not found!" };
            }

            return _mapper.Map<GetCauHoiByIdResponse>(existingCauHoi);
        }
    }
}
