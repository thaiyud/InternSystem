using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.Comunication.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class UpdateNhomZaloCommandHandler : IRequestHandler<UpdateNhomZaloCommandWrapper, UpdateNhomZaloResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateNhomZaloCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateNhomZaloResponse> Handle(UpdateNhomZaloCommandWrapper request, CancellationToken cancellationToken)
    {
        var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var nhomZalo = await _unitOfWork.NhomZaloRepository.GetByIdAsync(request.Id);

        if (nhomZalo == null)
        {
            return new UpdateNhomZaloResponse { IsSuccessful = false, ErrorMessage = "NhomZalo not found." };
        }

        nhomZalo.TenNhom = request.Command.TenNhom;
        nhomZalo.LinkNhom = request.Command.LinkNhom;
        nhomZalo.LastUpdatedBy = currentUserId;
        nhomZalo.LastUpdatedTime = DateTimeOffset.Now;
        nhomZalo.IsNhomChung = request.Command.IsNhomChung;
        try
        {
            _unitOfWork.NhomZaloRepository.UpdateNhomZaloAsync(nhomZalo);
            await _unitOfWork.SaveChangeAsync();
            return new UpdateNhomZaloResponse { IsSuccessful = true, Id = nhomZalo.Id };
        }
        catch (Exception ex)
        {
            // Log the exception
            return new UpdateNhomZaloResponse { IsSuccessful = false, ErrorMessage = $"Failed to update NhomZalo: {ex.Message}" };
        }
    }
}