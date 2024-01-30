using CertificateCreator.Services.CertificateTypeService;
using CertificateCreator.Services.EmployeesCertificateService;
using CertificateCreator.Services.EmployeeService;
using CertificateCreator.Services.FileService;
using CertificateCreator.Services.LoginService;

namespace CertificateCreator
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICertificateTypeService, CertificateTypeService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmployeesCertificateService, EmployeesCertificateService>();
            return services;
        }
    }
}
