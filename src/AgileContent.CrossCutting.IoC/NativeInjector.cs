using AgileContent.Application.Interface;
using AgileContent.Application.Service;
using AgileContent.Domain.FamilyNumber.Commands;
using AgileContent.Domain.FamilyNumber.Interface;
using AgileContent.Domain.NewCDNiTaas.Commands;
using AgileContent.Domain.NewCDNiTaas.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AgileContent.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Commands
            services.AddScoped<ICalcFamilyNumberCommand, CalcFamilyNumberCommand>();
            services.AddScoped<IConvertCdnToNowLogFileCommand, ConvertCdnToNowLogFileCommand>();
            services.AddScoped<IReadFileContentCommand, ReadFileContentCommand>();

            // Application
            services.AddScoped<IFamilyNumberService, FamilyNumberService>();
            services.AddScoped<INewCDNiTaasService, NewCDNiTaasService>();
        }
    }
}