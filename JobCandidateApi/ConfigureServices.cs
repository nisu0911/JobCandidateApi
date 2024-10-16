using Infrastructure;

namespace JobCandidateApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddInfrastructureServices(config);
            return services;
        }
    }
}