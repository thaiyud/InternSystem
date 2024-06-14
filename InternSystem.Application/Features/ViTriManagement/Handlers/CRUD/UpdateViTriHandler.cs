using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ViTriManagement.Commands;
using InternSystem.Application.Features.ViTriManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.ViTriManagement.Handlers.CRUD
{
    public class UpdateViTriHandler : IRequestHandler<UpdateViTriCommand, UpdateViTriResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateViTriHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateViTriResponse> Handle(UpdateViTriCommand request, CancellationToken cancellationToken)
        {
            ViTri? existingViTri = await _unitOfWork.ViTriRepository.GetByIdAsync(request.Id);
            if (existingViTri == null || existingViTri.IsDelete == true) return new UpdateViTriResponse() { Errors = "Vi Tri not found" };


            if (!(request.DuAnId < 0))
            {
                DuAn existingDA = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId!);
                if (existingDA == null) return new UpdateViTriResponse() { Errors = "Du An not found" };
            }

            existingViTri = _mapper.Map(request, existingViTri);

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<UpdateViTriResponse>(existingViTri);
        }
    }
}
