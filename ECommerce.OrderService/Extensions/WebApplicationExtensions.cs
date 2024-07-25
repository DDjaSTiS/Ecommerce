using FluentMigrator.Runner;

namespace ECommerce.OrderService.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void RunWithMigrate(this WebApplication app, string[] args)
        {
            if (args.Length > 0 && args[0].Equals("migrate", StringComparison.InvariantCultureIgnoreCase))
            {
                using var scope = app.Services.CreateScope();
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                runner.MigrateUp();
            }
            else
                app.Run();
        }
    }
}
