using Buhoborec.Infrastructure.Extensions;
using Buhoborec.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Buhoborec.WebUI.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var dbFile = app.Configuration.GetConnectionString("DefaultConnection")?
                .Replace("Data Source=", "", StringComparison.OrdinalIgnoreCase)
                .TrimEnd(';');
            dbFile = Path.GetFullPath(dbFile);
            if (!File.Exists(dbFile))
            {
                await context.Database.MigrateAsync();
            }

            await SeedAsync(context);
        }

        private static async Task SeedAsync(AppDbContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrderWithItemsAsync(context);
        }

        private static async Task SeedCustomerAsync(AppDbContext context)
        {
            if (!await context.WorkLogs.AnyAsync())
            {
                await context.WorkLogs.AddRangeAsync(InitialData.WorkLogs);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductAsync(AppDbContext context)
        {
            if (!await context.TaskItems.AnyAsync())
            {
                await context.TaskItems.AddRangeAsync(InitialData.TaskItems);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedOrderWithItemsAsync(AppDbContext context)
        {
            if (!await context.Absences.AnyAsync())
            {
                await context.Absences.AddRangeAsync(InitialData.Absences);
                await context.SaveChangesAsync();
            }
        }
    }
}
