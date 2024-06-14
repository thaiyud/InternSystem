
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Commands
{
    public class UpdateUserImageCommandHandler : IRequestHandler<UpdateUserImageCommand, UpdateUserImageResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserImageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateUserImageResponse> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new UpdateUserImageResponse { IsSuccess = false };
            }

            user.ImagePath = request.ImageUrl;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return new UpdateUserImageResponse { IsSuccess = true, ImageUrl = user.ImagePath };
        }
    }
}
