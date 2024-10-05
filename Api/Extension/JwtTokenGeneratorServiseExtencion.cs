using Api.Servise;

namespace Api.Extension
{
    public static class JwtTokenGeneratorServiseExtencion
    {
        public static IServiceCollection AddJwtTokenGenerator(this IServiceCollection services)
        {
            return services.AddScoped<JwtTokenGenerator>();
        }
    }
}
