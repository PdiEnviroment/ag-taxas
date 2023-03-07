using Hangfire;
using PDI.AG.Taxas.Services;

namespace PDI.AG.Taxas.Configuration
{
    public static class AppConfigure
    {
        public static WebApplication UseIOCHangFire(this WebApplication app)
        {
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(app.Services));

            return app;
        }

        public static void ScheduleTask(this WebApplication app)
        {
            RecurringJob.AddOrUpdate<ITaxaService>(
                "Consumidor de Taxas", 
                (taxaService) => taxaService.ConsumirTaxas(), 
                Cron.Minutely);
        }
    }
}
