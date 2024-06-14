using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Commands;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.CongNgheManagement.Handlers
{
    public class CreateCongNgheHandler : IRequestHandler<CreateCongNgheCommand, CreateCongNgheResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCongNgheHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateCongNgheResponse> Handle(CreateCongNgheCommand request, CancellationToken cancellationToken)
        {
            CongNghe? existingCN = _unitOfWork.CongNgheRepository.GetAllASync().Result.AsQueryable()
                .FirstOrDefault(d => d.Ten.Equals(request.Ten));

            if (existingCN != null && existingCN.IsDelete == false) return new CreateCongNgheResponse() { Errors = "Duplicate CongNghe name" };
            if (existingCN != null && existingCN.IsDelete == true)
            {
                existingCN.IsActive= true; 
                existingCN.IsDelete= false;
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<CreateCongNgheResponse>(existingCN);
            }
            CongNghe newCN = _mapper.Map<CongNghe>(request);
            newCN.CreatedBy = "Current user";
            newCN.LastUpdatedBy = newCN.CreatedBy;
            newCN.CreatedTime = DateTime.UtcNow.AddHours(7);
            newCN.LastUpdatedTime = DateTime.UtcNow.AddHours(7);
            newCN.DeletedTime = DateTime.UtcNow.AddHours(7);
            newCN.IsActive = true;
            newCN.IsDelete = false;
            newCN = await _unitOfWork.CongNgheRepository.AddAsync(newCN);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CreateCongNgheResponse>(newCN);
        }
    }
}
