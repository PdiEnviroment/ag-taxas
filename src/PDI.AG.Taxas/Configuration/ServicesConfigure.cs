using Hangfire;
using Hangfire.MemoryStorage;
using PDI.AG.Taxas.Services;

namespace PDI.AG.Taxas.Configuration
{
    public static class ServicesConfigure
    {
        public static IServiceCollection UseHangFire(this IServiceCollection services)
        {
            services.AddHangfire(op =>
            {
                op.UseMemoryStorage();
            });

            services.AddHangfireServer();

            return services;
        }

        public static IServiceCollection UseRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("RedisConnection");
                options.InstanceName = configuration.GetValue<string>("InstanceName");
            });

            return services;
        }

        public static IServiceCollection UseConsumeTaxas(this IServiceCollection services)
        {
            services.AddScoped<ITaxaService, TaxasService>();

            return services;
        }
    }
}
