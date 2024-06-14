using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Commands;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.Handlers.CRUD
{
    public class SelfUpdateInternInfoCommandHandler : IRequestHandler<SelfUpdateInternInfoCommand, UpdateInternInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SelfUpdateInternInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateInternInfoResponse> Handle(SelfUpdateInternInfoCommand request, CancellationToken cancellationToken)
        {
            var userId = await _unitOfWork.UserRepository.GetByIdAsync(request.LastUpdatedBy);
            request.Id = (int)userId.InternInfoId;

            InternInfo existingIntern = await _unitOfWork.InternInfoRepository.GetByIdAsync(request.Id);
            if (existingIntern == null || existingIntern.IsDelete)
                return new UpdateInternInfoResponse() { Errors = "Id Not found" };

            _mapper.Map(request, existingIntern);
            existingIntern.LastUpdatedTime = DateTimeOffset.Now;

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateInternInfoResponse>(existingIntern);
        }
    }
}
