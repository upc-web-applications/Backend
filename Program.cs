using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using RiskGuard.Platform.ReportsCompliance.Domain.Repositories;
using RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Seeding;
using RiskGuard.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("RiskGuardCors", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "server=localhost;user=root;password=12345678;database=riskguard-platform";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IMonthlyReportRepository, MonthlyReportRepository>();

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();
app.UseCors("RiskGuardCors");
app.UseHttpsRedirection();
app.MapControllers();

if (app.Configuration.GetValue("SeedDatabase", true))
{
    using var scope = app.Services.CreateScope();
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();
