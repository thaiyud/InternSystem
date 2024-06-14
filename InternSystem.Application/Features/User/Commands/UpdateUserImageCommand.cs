using InternSystem.Application.Features.User.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InternSystem.Application.Features.User.Commands
{
    public class UpdateUserImageCommand : IRequest<UpdateUserImageResponse>
    {
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
    }
    public class UpdateUserImageResponse
    {
        public bool IsSuccess { get; set; }
        public string ImageUrl { get; set; }
    }
}
