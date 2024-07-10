using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.UserDuAnManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.UserDuAnManagement.Queries;
using InternSystem.Domain.BaseException;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.ProjectAndTechnologyManagement.UserDuAnManagement.Handlers
{
    public class GetAllUserDuAnHandler : IRequestHandler<GetAllUserDuAnQuery, IEnumerable<GetAllUserDuAnResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUserDuAnHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllUserDuAnResponse>> Handle(GetAllUserDuAnQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<UserDuAn> allUserDuAn = _unitOfWork.UserDuAnRepository.Entities;
                IQueryable<UserDuAn> activeUserDuAn = allUserDuAn
                    .Where(p => !p.IsDelete)
                    .OrderByDescending(p => p.DuAnId)
                    .ThenByDescending(p => p.CreatedTime); ;
                var allUserDuAnList = await _unitOfWork.UserDuAnRepository.GetAllAsync()
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Không tìm thấy người dùng trong dự án");

                return _mapper.Map<IEnumerable<GetAllUserDuAnResponse>>(activeUserDuAn);
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
