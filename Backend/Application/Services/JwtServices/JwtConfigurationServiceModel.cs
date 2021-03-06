using Microsoft.Extensions.Configuration;
using System;

namespace Application.Services.JwtServices
{
    public interface IJwtConfigurationServiceModel
    {
        string Audience { get; }
        string Issuer { get; }
        IConfigurationSection JwtSection { get; }
        string Key { get; }
        TimeSpan AccessTokenValidationTime { get; }
    }

    class JwtConfigurationServiceModel : IJwtConfigurationServiceModel
    {
        private readonly IConfiguration _config;

        public JwtConfigurationServiceModel(IConfiguration config)
        {
            _config = config;
        }

        public IConfigurationSection JwtSection
        {
            get => _config.GetSection("JWT");
        }

        public string Issuer
        {
            get => JwtSection.GetSection("Issuer").Value;
        }

        public string Audience
        {
            get => JwtSection.GetSection("Audience").Value;
        }

        public string Key
        {
            get => JwtSection.GetSection("Key").Value;
        }

        public TimeSpan AccessTokenValidationTime => TimeSpan.Parse(JwtSection.GetSection("AccessTokenValidationTime").Value);
    }
}
