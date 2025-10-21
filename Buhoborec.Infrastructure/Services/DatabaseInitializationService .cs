
using Buhoborec.Infrastructure.Extensions;
using Buhoborec.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Buhoborec.Infrastructure.Services
{
    public class DatabaseInitializationService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DatabaseInitializationService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var dbFile = _configuration.GetConnectionString("DefaultConnection")?
                .Replace("Data Source=", "", StringComparison.OrdinalIgnoreCase)
                .TrimEnd(';');
            dbFile = Path.GetFullPath(dbFile);

            if (!File.Exists(dbFile))
            {
                await context.Database.MigrateAsync(cancellationToken);
            }

            await SeedAsync(context);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task SeedAsync(AppDbContext context)
        {
            if (!await context.WorkLogs.AnyAsync())
                await context.WorkLogs.AddRangeAsync(InitialData.WorkLogs);

            if (!await context.TaskItems.AnyAsync())
                await context.TaskItems.AddRangeAsync(InitialData.TaskItems);

            if (!await context.Absences.AnyAsync())
                await context.Absences.AddRangeAsync(InitialData.Absences);

            await context.SaveChangesAsync();
        }
    }
}
