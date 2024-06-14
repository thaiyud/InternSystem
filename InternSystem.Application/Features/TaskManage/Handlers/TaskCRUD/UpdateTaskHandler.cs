using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.TaskManage.Commands.Update;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.TaskCRUD
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            Tasks? exist = await _unitOfWork.TaskRepository.GetByIdAsync(request.Id);
            if (exist == null || exist.IsDelete == true)
                throw new ArgumentNullException(
                   nameof(request.Id),
                   $"Task {request.Id} is not exist");

            if (request.DuAnId!=exist.DuAnId)
            {
                DuAn? duAn = await _unitOfWork.DuAnRepository.GetByIdAsync(request.DuAnId);
                if (duAn == null || duAn.IsDelete == true)
                    throw new ArgumentNullException(
                  nameof(request.DuAnId),
                  $"Du an id {request.DuAnId} is not exist");
                exist.DuAnId = (int)request.DuAnId;
            }
            if (request.HanHoanThanh < request.NgayGiao
                || exist.NgayGiao > request.HanHoanThanh
                || request.NgayGiao > exist.HanHoanThanh)
                throw new ArgumentNullException(
                  nameof(request),
                  $"Ngay Giao must lower than {request.HanHoanThanh}");
            if (!string.IsNullOrWhiteSpace(request.MoTa))
                exist.MoTa = request.MoTa;
            if (!string.IsNullOrWhiteSpace(request.NoiDung))
                exist.NoiDung = request.NoiDung;
            if (request.NgayGiao.HasValue)
                exist.NgayGiao = request.NgayGiao.Value;
            if (request.HanHoanThanh.HasValue)
                exist.HanHoanThanh = request.HanHoanThanh.Value;
            if (request.HoanThanh.HasValue)
                exist.HoanThanh = request.HoanThanh.Value;
            exist.LastUpdatedTime = DateTimeOffset.Now;
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<TaskResponse>(exist);
        }
    }
}
