
using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;

public class GetFilteredInternInfoQueryHandler : IRequestHandler<GetFilteredInternInfoQuery, IEnumerable<InternInfo>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFilteredInternInfoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<InternInfo>> Handle(GetFilteredInternInfoQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.InternInfoRepository.GetFilterdInternInfosAsync(request.SchoolId, request.StartDate, request.EndDate);
    }
}