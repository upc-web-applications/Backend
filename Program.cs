using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.EntityFrameworkCore.Extensions;
using RiskGuard.Platform.Iam.Application.Internal.OutboundServices;
using RiskGuard.Platform.Iam.Application.Internal.Services;
using RiskGuard.Platform.Iam.Domain.Repositories;
using RiskGuard.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using RiskGuard.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using RiskGuard.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using RiskGuard.Platform.Inspections.Application.Internal.Services;
using RiskGuard.Platform.Inspections.Domain.Repositories;
using RiskGuard.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.Mitigation.Application.Internal.Services;
using RiskGuard.Platform.Mitigation.Domain.Repositories;
using RiskGuard.Platform.Mitigation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.MonitoringDashboard.Domain.Repositories;
using RiskGuard.Platform.MonitoringDashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.OrganizationAssets.Domain.Repositories;
using RiskGuard.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.ReportsCompliance.Domain.Repositories;
using RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.RiskAssessment.Application.Internal.Services;
using RiskGuard.Platform.RiskAssessment.Domain.Repositories;
using RiskGuard.Platform.RiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Seeding;
using RiskGuard.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;
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

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
var tokenSecret = builder.Configuration["TokenSettings:Secret"] ?? string.Empty;
var tokenKey = Encoding.ASCII.GetBytes(tokenSecret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IInspeccionRepository, InspeccionRepository>();
builder.Services.AddScoped<IEvaluacionRiesgoRepository, EvaluacionRiesgoRepository>();
builder.Services.AddScoped<ITicketAccionCorrectivaRepository, TicketAccionCorrectivaRepository>();
builder.Services.AddScoped<IHeatMapZoneRepository, HeatMapZoneRepository>();
builder.Services.AddScoped<IMonthlyReportRepository, MonthlyReportRepository>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IamCommandService>();
builder.Services.AddScoped<InspectionCommandService>();
builder.Services.AddScoped<RiskAssessmentService>();
builder.Services.AddScoped<MitigationCommandService>();

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();
app.UseCors("RiskGuardCors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Configuration.GetValue("SeedDatabase", true))
{
    using var scope = app.Services.CreateScope();
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();
