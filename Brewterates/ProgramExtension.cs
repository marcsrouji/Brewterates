using Brewterates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Brewterates
{
    public static class ProgramExtension
    {
        public static void ConfigureSql (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<brewteratesDbContext>(options =>
            {
                options.UseSqlServer(config["ConnectionStrings:SqlServer"].ToString());
            });
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(options => 
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddJsonOptions(options => 
                options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull);
        }
    }
}
