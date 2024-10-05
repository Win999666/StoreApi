using Api.Common;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Api.Extension
{
    public static class RoleInicializerServiceExtenction
    {
        public static async Task InicializeRoleAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in SharedData.Roles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    }
}
