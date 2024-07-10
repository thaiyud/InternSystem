using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Queries;
using InternSystem.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.DuAnManagement.Handlers
{
    public class GetAllDuAnHandler : IRequestHandler<GetAllDuAnQuery, IEnumerable<GetAllDuAnResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllDuAnResponse>> Handle(GetAllDuAnQuery request, CancellationToken cancellationToken)
        {
            var listDuAn = await _unitOfWork.DuAnRepository.GetAllAsync();
            var filteredDuAns = listDuAn.Where(da => da.IsActive && !da.IsDelete);
            if (listDuAn == null || !listDuAn.Any())
                throw new ErrorException(StatusCodes.Status204NoContent, ErrorCode.NotFound, "Không có Dự Án");

            return _mapper.Map<IEnumerable<GetAllDuAnResponse>>(listDuAn);
        }
    }
}
