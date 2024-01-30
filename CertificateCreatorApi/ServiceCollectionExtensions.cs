using CertificateCreatorApi.Repositories.CertificateTypeRepository;
using CertificateCreatorApi.Repositories.EmployeeRepository;
using CertificateCreatorApi.Repositories.EmployeesCertificateRepository;
using CertificateCreatorApi.Repositories.LoginRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CertificateCreatorApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ICertificateTypeRepository, CertificateTypeRepository>();
            services.AddScoped<IEmployeesCertificateRepository, EmployeesCertificateRepository>();
            return services;
        }
    }
}
