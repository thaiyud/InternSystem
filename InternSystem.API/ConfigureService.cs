using InternSystem.API.Utilities;
using InternSystem.Application.Common.EmailService;

using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using Microsoft.AspNetCore.Identity;
using System;

namespace InternSystem.API
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
