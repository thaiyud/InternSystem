using FluentValidation;
using InternSystem.Application.Common.Behaviors;
using InternSystem.Application.Common.Mapping;
using InternSystem.Application.Features.Comunication.Commands.ChatCommands;
using InternSystem.Application.Features.Interview.Handlers;
using InternSystem.Application.Features.Message.Handlers;
using InternSystem.Application.Features.Token.Handlers;
using InternSystem.Application.Features.Token.Models;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Handlers;
using InternSystem.Application.Features.User.Models.LoginModels;
using InternSystem.Application.Features.User.Models.ResetTokenModels;
using InternSystem.Application.Features.User.Utility;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

public static class ConfigureService
{
    public static IServiceCollection ConfigureApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationErrorBehaviour<,>));
        });

        PasswordGenerator.Initialize(configuration);

        services.AddSingleton<TokenValidationParameters>(provider =>
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = configuration["Jwt:Issuer"],
                ValidIssuer = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                ClockSkew = TimeSpan.FromMinutes(60)
            };
        });
        services.AddTransient<IRequestHandler<LoginCommand, LoginResponse>, LoginCommandHandler>();
        services.AddTransient<IRequestHandler<RefreshTokenCommand, TokenResponse>, RefreshTokenHadler>();
        services.AddTransient<IRequestHandler<ResetTokenCommand, ResetTokenResponse>, ResetTokenCommandHandler>();
        services.AddTransient<IRequestHandler<SendMessageCommand, bool>, SendMessageHandler>();

        services.AddSignalR();
        return services;
    }
}