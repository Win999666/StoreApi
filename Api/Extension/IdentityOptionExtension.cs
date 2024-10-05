using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Api.Extension
{
    public static class IdentityOptionExtension
    {
        public static IServiceCollection AddConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            return services;
        }
    }
}
