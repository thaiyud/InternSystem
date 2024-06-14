using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.PhongVanManagement.Commands;
using InternSystem.Application.Features.PhongVanManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.PhongVanManagement.Handlers
{
    public class CreatePhongVanHandler : IRequestHandler<CreatePhongVanCommand, CreatePhongVanResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePhongVanHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreatePhongVanResponse> Handle(CreatePhongVanCommand request, CancellationToken cancellationToken)
        {
            var passingRank = _configuration.GetValue<decimal>("PhongVanManagement:PassingRank");
            var notPass = _configuration.GetValue<string>("PhongVanManagement:NotPass");

            // Map request to PhongVan entity
            PhongVan newPhongVan = _mapper.Map<PhongVan>(request);
            newPhongVan.LastUpdatedTime = newPhongVan.CreatedTime;
            newPhongVan.LastUpdatedBy = newPhongVan.CreatedBy;
            newPhongVan.DeletedBy = "";

            if (newPhongVan.Rank <= passingRank)
            {
                var lichPhongVanExisting = await _unitOfWork.LichPhongVanRepository.GetByIdAsync(newPhongVan.IdLichPhongVan);
                lichPhongVanExisting.KetQua = notPass;
                await _unitOfWork.LichPhongVanRepository.UpdateAsync(lichPhongVanExisting);
            }

            // Add the new PhongVan to the repository and save changes
            newPhongVan = await _unitOfWork.PhongVanRepository.AddAsync(newPhongVan);
            await _unitOfWork.SaveChangeAsync();

            // Map the newly created PhongVan entity to CreatePhongVanResponse and return
            return _mapper.Map<CreatePhongVanResponse>(newPhongVan);
        }
    }
}
