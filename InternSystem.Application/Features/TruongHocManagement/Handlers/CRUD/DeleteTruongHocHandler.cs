using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TruongHocManagement.Commands;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.TruongHocManagement.Handlers.CRUD
{
    public class DeleteTruongHocHandler : IRequestHandler<DeleteTruongHocCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteTruongHocHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteTruongHocCommand request, CancellationToken cancellationToken)
        {
            TruongHoc? searchResult = await _unitOfWork.TruongHocRepository.GetByIdAsync(request.Id);

            if (searchResult == null || searchResult.IsDelete == true) return false;

            searchResult.DeletedBy = request.DeletedBy;
            searchResult.DeletedTime = DateTime.UtcNow.AddHours(7);
            searchResult.IsActive = false;
            searchResult.IsDelete = true;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
