using AgileContent.Application.Interface;
using AgileContent.Application.Service;
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

            // Application
            services.AddScoped<IFamilyNumber, FamilyNumber>();

            // Domain - Commands
            //services.AddScoped<IPlayerDomain, PlayerDomain>();
            //services.AddScoped<IGameDomain, GameDomain>();
        }
    }
}