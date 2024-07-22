using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheManagement.Queries;
using InternSystem.Application.Features.QuestionManagement.CauHoiManagement.Models;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheManagement.Handlers
{
    public class GetCongNghesByTenQueryHandler : IRequestHandler<GetCongNghesByTenQuery, IEnumerable<GetCongNgheByTenResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCongNghesByTenQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCongNgheByTenResponse>> Handle(GetCongNghesByTenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //var congNghe = (await _unitOfWork.CongNgheRepository.GetCongNghesByTenAsync(request.Ten)
                //    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Công nghệ không tồn tại."));
                //var result = _mapper.Map<IEnumerable<GetCongNgheByTenResponse>>(congNghe);
                //return result;

                var repository = _unitOfWork.GetRepository<CongNghe>();
                var congNgheQuery = repository.GetAllQueryable();

                var congNgheByTen = await repository.ToListAsync(
                    congNgheQuery.Where(c => c.Ten.Contains(request.Ten) && !c.IsDelete),
                    cancellationToken
                );

                if (!congNgheByTen.Any())
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy công nghệ hợp lệ.");
                }

                var result = _mapper.Map<IEnumerable<GetCongNgheByTenResponse>>(congNgheByTen);
                return result;
            }
            catch (ErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, ResponseCodeConstants.INTERNAL_SERVER_ERROR, "Đã có lỗi xảy ra");
            }
        }
    }
}
