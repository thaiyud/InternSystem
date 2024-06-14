using InternSystem.Application.Features.User.Models.LoginModels;
using MediatR;


namespace InternSystem.Application.Features.Token.Models
{
    public class RefreshTokenCommand : IRequest<TokenResponse>
    {
        public RefreshTokenRequest RefreshToken { get; set; }
        public RefreshTokenCommand(RefreshTokenRequest loginRequest)
    {
            RefreshToken = loginRequest;
        }
    }
}
