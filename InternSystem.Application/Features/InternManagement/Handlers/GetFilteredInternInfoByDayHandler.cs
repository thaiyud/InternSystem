
using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;

public class GetFilteredInternInfoByDayHandler : IRequestHandler<GetFilteredInternInfoByDayQuery, IEnumerable<InternInfo>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFilteredInternInfoByDayHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<InternInfo>> Handle(GetFilteredInternInfoByDayQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.InternInfoRepository.GetFilteredInternInfoByDaysAsync(request.Day);
    }
}