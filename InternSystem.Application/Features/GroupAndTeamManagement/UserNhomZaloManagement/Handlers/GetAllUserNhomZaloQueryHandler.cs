using AutoMapper;
using InternSystem.Application.Common.Constants;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.GroupAndTeamManagement.UserNhomZaloManagement.Models;
using InternSystem.Application.Features.GroupAndTeamManagement.UserNhomZaloManagement.Queries;
using InternSystem.Domain.BaseException;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.GroupAndTeamManagement.UserNhomZaloManagement.Handlers
{
    public class GetAllUserNhomZaloQueryHandler : IRequestHandler<GetAllUserNhomZaloQuery, IEnumerable<GetUserNhomZaloResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserNhomZaloQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUserNhomZaloResponse>> Handle(GetAllUserNhomZaloQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userNhomZaloEntities = await _unitOfWork.UserNhomZaloRepository.GetAllAsync();
                var userNhomZaloResponses = _mapper.Map<IEnumerable<GetUserNhomZaloResponse>>(userNhomZaloEntities);
                return userNhomZaloResponses;
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
