using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using InternSystem.Application.Features.KyThucTapManagement.Queries;
using InternSystem.Application.Features.Search.Models;
using InternSystem.Application.Features.Search.Queries;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.Search.Handlers
{
    public class GetInternInfoByTruongHocNameQueryHandler : IRequestHandler<GetInternInfoByTruongHocNameQuery, IEnumerable<GetInternInfoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInternInfoByTruongHocNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetInternInfoResponse>> Handle(GetInternInfoByTruongHocNameQuery request, CancellationToken cancellationToken)
        {
            var internInfos = await _unitOfWork.InternInfoRepository.GetInternInfoByTenTruongHocAsync(request.TruongHocName);
            return _mapper.Map<IEnumerable<GetInternInfoResponse>>(internInfos);
        }
    }
}
