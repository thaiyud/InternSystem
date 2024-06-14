using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ThongBaoManagement.Commands;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Handlers
{
    public class UpdateThongBaoHandler : IRequestHandler<UpdateThongBaoCommand, UpdateThongBaoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateThongBaoHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateThongBaoResponse> Handle(UpdateThongBaoCommand request, CancellationToken cancellationToken)
        {
            ThongBao? existingTB = await _unitOfWork.ThongBaoRepository.GetByIdAsync(request.Id);

            if (existingTB == null) return new UpdateThongBaoResponse() { Errors = "Id not found" };

            existingTB = _mapper.Map(request, existingTB);
            existingTB.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateThongBaoResponse>(existingTB);    
        }
    }
}
