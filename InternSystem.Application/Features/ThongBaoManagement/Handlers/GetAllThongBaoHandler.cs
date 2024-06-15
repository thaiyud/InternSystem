using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using InternSystem.Application.Features.ThongBaoManagement.Queries;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.ThongBaoManagement.Handlers
{
    public class GetAllThongBaoHandler : IRequestHandler<GetAllThongBaoQuery, GetAllThongBaoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllThongBaoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllThongBaoResponse> Handle(GetAllThongBaoQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllThongBaoResponse();

            response.ThongBaos = await _unitOfWork.ThongBaoRepository.GetAllAsync();

            if (request.PageNumber > 0 && request.PageSize > 0)
            {
                int skipCount = (int) ((request.PageNumber - 1) * request.PageSize);
                response.ThongBaos = response.ThongBaos.Skip(skipCount).Take( (int)request.PageSize);
            }

            return response;
        }
    }
}
