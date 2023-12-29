using Brewterates.Application.Abstractions;
using Brewterates.Application.Abstractions.Discount;
using Brewterates.Application.Services;
using Brewterates.Application.Services.Discount;
using Microsoft.Extensions.DependencyInjection;

namespace Brewterates.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<Mapper>();
            services.AddScoped<IBeerService,BeerService>();
            services.AddScoped<IWholesalerSevice, WholesalerSevice>();
            services.AddScoped<IDiscountFactory, DiscountFactory>();
            services.AddScoped<IQuoteService, QuoteService>();
        } 
    }
}
