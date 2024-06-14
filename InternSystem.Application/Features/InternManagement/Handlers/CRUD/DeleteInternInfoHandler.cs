using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.InternManagement.Handlers.CRUD
{
    public class DeleteInternInfoHandler : IRequestHandler<DeleteInternInfoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteInternInfoHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteInternInfoCommand request, CancellationToken cancellationToken)
        {
            InternInfo? intern = await _unitOfWork.InternInfoRepository.GetByIdAsync(request.Id);
            if (intern == null || intern.IsDelete == true) { return false; }

            intern.DeletedBy = request.DeletedBy;
            intern.DeletedTime = DateTimeOffset.Now;
            intern.IsActive = false;
            intern.IsDelete = true;
            await _unitOfWork.SaveChangeAsync();

            return true;
        }
    }
}
