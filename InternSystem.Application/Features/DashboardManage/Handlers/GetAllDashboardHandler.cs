using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DashboardManage.Models;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.DashboardManage.Handlers
{
    public class GetAllDashboardHandler : IRequestHandler<GetAllDashboardQuery, GetAllDashboardResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDashboardHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllDashboardResponse> Handle(GetAllDashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboardList = await _unitOfWork.DashboardRepository.GetAllASync();
            while (!dashboardList.Any())
            {
                var newDashboard = new Dashboard();
                await _unitOfWork.DashboardRepository.AddAsync(newDashboard);
                await _unitOfWork.SaveChangeAsync();

                dashboardList = await _unitOfWork.DashboardRepository.GetAllASync();
            }
            var firstDashboard = dashboardList.First();

            firstDashboard.Interviewed = await _unitOfWork.PhongVanRepository.GetAllInterviewed();

            firstDashboard.ReceivedCV = await _unitOfWork.InternInfoRepository.GetAllReceivedCV();

            firstDashboard.Passed = await _unitOfWork.PhongVanRepository.GetAllPassed();

            firstDashboard.Interning = await _unitOfWork.InternInfoRepository.GetAllInterning();

            firstDashboard.Interned = await _unitOfWork.InternInfoRepository.GetAllInterned();

            if (dashboardList.Count() == 0)
            {
                await _unitOfWork.DashboardRepository.AddAsync(firstDashboard);
            }
            else
            {
                await _unitOfWork.DashboardRepository.UpdateAsync(firstDashboard);
            }

            await _unitOfWork.SaveChangeAsync();

            return new GetAllDashboardResponse
            {
                ReceivedCV = firstDashboard.ReceivedCV,
                Interviewed = firstDashboard.Interviewed,
                Passed = firstDashboard.Passed,
                Interning = firstDashboard.Interning,
                Interned = firstDashboard.Interned
            };
        }
    }
}
