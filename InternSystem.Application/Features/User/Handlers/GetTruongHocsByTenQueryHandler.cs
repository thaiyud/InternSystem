using AutoMapper;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Queries;
using InternSystem.Domain.Entities;
using MediatR;

namespace InternSystem.Application.Features.User.Handlers
{
    public class GetTruongHocsByTenQueryHandler : IRequestHandler<GetTruongHocByTenQuery, IEnumerable<TruongHoc>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTruongHocsByTenQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TruongHoc>> Handle(GetTruongHocByTenQuery request, CancellationToken cancellationToken)
        {
            var truongHocs = await _unitOfWork.TruongHocRepository.GetTruongHocsByTenAsync(request.Ten);
            Console.WriteLine(truongHocs);
            return _mapper.Map<IEnumerable<GetTruongHocByNameResponse>>(truongHocs);
        }
    }
}
