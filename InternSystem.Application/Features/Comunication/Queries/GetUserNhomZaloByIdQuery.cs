using MediatR;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Application.Features.Comunication.Models;

namespace InternSystem.Application.Features.Comunication.Queries
{
    public class GetUserNhomZaloByIdQuery : IRequest<GetUserNhomZaloResponse>
    {
        public int Id { get; }

        public GetUserNhomZaloByIdQuery(int id)
        {
            Id = id;
        }
    }
}
