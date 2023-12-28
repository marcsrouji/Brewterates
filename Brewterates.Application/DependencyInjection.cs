using Brewterates.Application.Abstractions;
using Brewterates.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Brewterates.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<Mapper>();
            services.AddScoped<IBeerService,BeerService>();
        } 
    }
}
