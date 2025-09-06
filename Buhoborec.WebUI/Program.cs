using Buhoborec.Application.Common.Behaviors;
using Buhoborec.Application.Common.Extensions;
using Buhoborec.Application.Common.Interfaces;
using Buhoborec.Application.Mappings;
using Buhoborec.Application.Tasks.Validators;
using Buhoborec.Infrastructure.Persistence;
using Buhoborec.Infrastructure.Services;
using Buhoborec.WebUI.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
       .AddCircuitOptions(options =>
       {
           if (builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Internal")
           {
               options.DetailedErrors = true;
           }
       });

// DbContext
var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=Buhoborec.db";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(conn));

// Mapping
builder.Services.AddMapsterConfiguration();

// MediatR
builder.Services.AddMediatR(Assembly.Load("Buhoborec.Application"));

// Pipeline behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Validation
builder.Services.AddApplicationValidators(typeof(CreateTaskValidator).Assembly);

// Services
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IDateTime, SystemDateTime>();

// UI
builder.Services.AddMudServices();

var app = builder.Build();

// Migrate database
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

ProgramScoped.ServiceProvider = app.Services;

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

public class SystemDateTime : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public static class ProgramScoped
{
    public static IServiceProvider ServiceProvider { get; set; } = default!;
}
