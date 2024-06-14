using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiManagement.Commands;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CauHoiManagement.Handlers.CRUD
{
    public class UpdateCauHoiHandler : IRequestHandler<UpdateCauHoiCommand, UpdateCauHoiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCauHoiHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateCauHoiResponse> Handle(UpdateCauHoiCommand request, CancellationToken cancellationToken)
        {
            CauHoi? existingCauHoi = await _unitOfWork.CauHoiRepository.GetByIdAsync(request.Id);
            if(existingCauHoi == null || !existingCauHoi.IsActive)
            {
                return new UpdateCauHoiResponse() {Errors = "Cau Hoi not found!" };
            }

            existingCauHoi = _mapper.Map(request, existingCauHoi);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<UpdateCauHoiResponse>(existingCauHoi);
        }
    }
}
