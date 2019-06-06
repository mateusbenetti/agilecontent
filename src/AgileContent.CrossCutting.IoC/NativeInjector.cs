using AgileContent.BussinessLogic;
using AgileContent.BussinessLogic.Interface;
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

            //BussinessLogic - FamilyNumberService
            services.AddScoped<IFamilyNumber, FamilyNumber>();
            services.AddScoped<INewCDN, NewCDN>();
        }
    }
}