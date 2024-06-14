using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using InternSystem.Application.Features.ThongBaoManagement.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Handlers
{
    public class GetThongBaoByIdHandler : IRequestHandler<GetThongBaoByIdQuery, GetThongBaoByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetThongBaoByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetThongBaoByIdResponse> Handle(GetThongBaoByIdQuery request, CancellationToken cancellationToken)
        {
            ThongBao? result = await _unitOfWork.ThongBaoRepository.GetByIdAsync(request.Id);

            if (result == null) return new GetThongBaoByIdResponse() { Errors = "Id not found" };

            return _mapper.Map<GetThongBaoByIdResponse>(result);
        }
    }
}
