using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Queries;
using InternSystem.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.TaskManage.Handlers.NhomZaloTaskCRUD
{
    public class GetNhomZaloTaskHandler : IRequestHandler<GetNhomZaloTaskByQuery, IEnumerable<NhomZaloTaskReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetNhomZaloTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NhomZaloTaskReponse>> Handle(GetNhomZaloTaskByQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exist = await _unitOfWork.NhomZaloTaskRepository.GetAllAsync();
                if (exist == null || !exist.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy");
                }

                return _mapper.Map<IEnumerable<NhomZaloTaskReponse>>(exist);
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Lỗi lấy danh sách task trong nhóm Zalo");
            }
        }
    
    }
}
