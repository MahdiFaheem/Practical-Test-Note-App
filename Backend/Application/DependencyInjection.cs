using Application.DTOs.ResponseDTOs;
using Application.Interfaces.EncyptionInterfaces;
using Application.Managers;
using Application.Repositories;
using Application.Services;
using Application.Services.EncryptionServices;
using Application.Services.JsonFileService;
using Application.Services.JwtServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            #region Jwt Service

            var jwtSection = config.GetSection("JWT");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection.GetSection("Issuer").Value,
                    ValidAudience = jwtSection.GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.GetSection("Key").Value))
                };
            });

            #endregion

            #region Application Service

            // Adding application services.
            services.AddTransient<IAuthenticationService, JwtService>();
            services.AddTransient<IEncryptionService, BCryptEncryptionService>();
            services.AddTransient<IJwtConfigurationServiceModel, JwtConfigurationServiceModel>();
            services.AddTransient<IJsonFileService, JsonFileService>();

            services.AddAutoMapper(typeof(DependencyInjection));

            #endregion

            #region Repository Service

            // Adding repository services.
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<INoteRepository, NoteRepository>();

            #endregion

            #region Manager Service

            // Adding manager services
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<INoteManager, NoteManager>();

            services.AddTransient<IApiResponseDTO, ApiResponseDTO>();

            #endregion

            return services;
        }
    }
}
