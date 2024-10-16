using Infrastructure;

namespace JobCandidateApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddInfrastructureServices();
            return services;
        }
    }
}