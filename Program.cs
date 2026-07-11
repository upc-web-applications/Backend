using System.Text;
using System.Text.Json;
using MySqlConnector;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Acme.Center.Platform.Hazards.Application.CommandServices;
using Acme.Center.Platform.Hazards.Application.Internal.CommandServices;
using Acme.Center.Platform.Hazards.Application.Internal.QueryServices;
using Acme.Center.Platform.Hazards.Application.QueryServices;
using Acme.Center.Platform.Hazards.Domain.Repositories;
using Acme.Center.Platform.Hazards.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.Iam.Application.CommandServices;
using Acme.Center.Platform.Iam.Application.Internal.CommandServices;
using Acme.Center.Platform.Iam.Application.Internal.OutboundServices;
using Acme.Center.Platform.Iam.Application.Internal.QueryServices;
using Acme.Center.Platform.Iam.Application.QueryServices;
using Acme.Center.Platform.Iam.Domain.Repositories;
using Acme.Center.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using Acme.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using Acme.Center.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using Acme.Center.Platform.Mitigations.Application.CommandServices;
using Acme.Center.Platform.Mitigations.Application.Internal.CommandServices;
using Acme.Center.Platform.Mitigations.Application.Internal.QueryServices;
using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.ReportsCompliance.Application.CommandServices;
using Acme.Center.Platform.ReportsCompliance.Application.Internal.CommandServices;
using Acme.Center.Platform.ReportsCompliance.Application.Internal.QueryServices;
using Acme.Center.Platform.ReportsCompliance.Application.QueryServices;
using Acme.Center.Platform.ReportsCompliance.Domain.Repositories;
using Acme.Center.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.RiskAssessments.Application.CommandServices;
using Acme.Center.Platform.RiskAssessments.Application.Internal.CommandServices;
using Acme.Center.Platform.RiskAssessments.Application.Internal.QueryServices;
using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;
using Acme.Center.Platform.RiskAssessments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.Hazards.Interfaces.Acl;
using Acme.Center.Platform.Hazards.Application.Acl;
using Acme.Center.Platform.Iam.Interfaces.Acl;
using Acme.Center.Platform.Iam.Application.Acl;
using Acme.Center.Platform.Technicians.Interfaces.Acl;
using Acme.Center.Platform.Technicians.Application.Acl;
using Acme.Center.Platform.RiskAssessments.Interfaces.Acl;
using Acme.Center.Platform.RiskAssessments.Application.Acl;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Mediator.InProcess;
using Acme.Center.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Seeding;
using Acme.Center.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Acme.Center.Platform.Technicians.Application.CommandServices;
using Acme.Center.Platform.Technicians.Application.Internal.CommandServices;
using Acme.Center.Platform.Technicians.Application.Internal.QueryServices;
using Acme.Center.Platform.Technicians.Application.QueryServices;
using Acme.Center.Platform.Technicians.Domain.Repositories;
using Acme.Center.Platform.Technicians.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.Inspections.Application.Internal.Services;
using Acme.Center.Platform.Inspections.Domain.Repositories;
using Acme.Center.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.MonitoringDashboard.Application.Internal.Services;
using Acme.Center.Platform.MonitoringDashboard.Domain.Repositories;
using Acme.Center.Platform.MonitoringDashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Acme.Center.Platform.OrganizationAssets.Domain.Repositories;
using Acme.Center.Platform.OrganizationAssets.Application.Internal.Services;
using Acme.Center.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using ProblemDetailsFactory = Acme.Center.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Controllers with kebab-case + JSON options
builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new KebabCaseRouteNamingConvention());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddLocalization();
builder.Services.AddEndpointsApiExplorer();

// Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RiskGuard Platform API", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName ?? type.Name);
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("RiskGuardCors", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "server=localhost;user=root;password=12345678;database=riskguard-platform";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

// JWT Authentication
var tokenSettingsSection = builder.Configuration.GetSection("TokenSettings");
builder.Services.Configure<TokenSettings>(tokenSettingsSection);
var secret = tokenSettingsSection["Secret"] ?? "RiskGuardDefaultSecretKey2026!@#$%";
var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OperatorOrSupervisor", policy => policy.RequireRole("Operator", "Supervisor"));
    options.AddPolicy("SupervisorOnly", policy => policy.RequireRole("Supervisor"));
    options.AddPolicy("SupervisorOrAdministrator", policy => policy.RequireRole("Supervisor", "Administrator"));
    options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("Administrator"));
});

// Shared DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ProblemDetailsFactory>();

// ReportsCompliance DI
builder.Services.AddScoped<IBaseReportsRepository, BaseReportsRepository>();
builder.Services.AddScoped<IReportsComplianceCommandService, ReportsComplianceCommandService>();
builder.Services.AddScoped<IReportsComplianceQueryService, ReportsComplianceQueryService>();

// IAM DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Hazard DI
builder.Services.AddScoped<IHazardRepository, HazardRepository>();
builder.Services.AddScoped<IHazardCommandService, HazardCommandService>();
builder.Services.AddScoped<IHazardQueryService, HazardQueryService>();

// Technician DI
builder.Services.AddScoped<ITechnicianRepository, TechnicianRepository>();
builder.Services.AddScoped<ITechnicianCommandService, TechnicianCommandService>();
builder.Services.AddScoped<ITechnicianQueryService, TechnicianQueryService>();

// RiskAssessment DI
builder.Services.AddScoped<IRiskAssessmentRepository, RiskAssessmentRepository>();
builder.Services.AddScoped<IRiskAssessmentCommandService, RiskAssessmentCommandService>();
builder.Services.AddScoped<IRiskAssessmentQueryService, RiskAssessmentQueryService>();
builder.Services.AddScoped<IRiskPatternRepository, RiskPatternRepository>();
builder.Services.AddScoped<IRiskPatternCommandService, RiskPatternCommandService>();
builder.Services.AddScoped<IRiskPatternQueryService, RiskPatternQueryService>();
builder.Services.AddScoped<IPatternAlertRepository, PatternAlertRepository>();
builder.Services.AddScoped<IPatternAlertCommandService, PatternAlertCommandService>();
builder.Services.AddScoped<IPatternAlertQueryService, PatternAlertQueryService>();
builder.Services.AddScoped<IAreaCriticalityLevelRepository, AreaCriticalityLevelRepository>();
builder.Services.AddScoped<IAreaCriticalityLevelCommandService, AreaCriticalityLevelCommandService>();
builder.Services.AddScoped<IAreaCriticalityLevelQueryService, AreaCriticalityLevelQueryService>();
builder.Services.AddScoped<IDailySummaryRepository, DailySummaryRepository>();
builder.Services.AddScoped<IDailySummaryCommandService, DailySummaryCommandService>();
builder.Services.AddScoped<IDailySummaryQueryService, DailySummaryQueryService>();

// Mitigation DI
builder.Services.AddScoped<IMitigationRepository, MitigationRepository>();
builder.Services.AddScoped<IMitigationCommandService, MitigationCommandService>();
builder.Services.AddScoped<IMitigationQueryService, MitigationQueryService>();
builder.Services.AddScoped<ICorrectiveActionTicketRepository, CorrectiveActionTicketRepository>();
builder.Services.AddScoped<ICorrectiveActionTicketCommandService, CorrectiveActionTicketCommandService>();
builder.Services.AddScoped<ICorrectiveActionTicketQueryService, CorrectiveActionTicketQueryService>();
builder.Services.AddScoped<IMeasureVerificationRepository, MeasureVerificationRepository>();
builder.Services.AddScoped<IMeasureVerificationCommandService, MeasureVerificationCommandService>();
builder.Services.AddScoped<IMeasureVerificationQueryService, MeasureVerificationQueryService>();
builder.Services.AddScoped<ITicketHistoryRepository, TicketHistoryRepository>();
builder.Services.AddScoped<ITicketHistoryCommandService, TicketHistoryCommandService>();
builder.Services.AddScoped<ITicketHistoryQueryService, TicketHistoryQueryService>();
builder.Services.AddScoped<ISlaAlertRepository, SlaAlertRepository>();
builder.Services.AddScoped<ISlaAlertCommandService, SlaAlertCommandService>();
builder.Services.AddScoped<ISlaAlertQueryService, SlaAlertQueryService>();
builder.Services.AddScoped<ICriticalNotificationRepository, CriticalNotificationRepository>();
builder.Services.AddScoped<ICriticalNotificationCommandService, CriticalNotificationCommandService>();
builder.Services.AddScoped<ICriticalNotificationQueryService, CriticalNotificationQueryService>();

// Inspections DI
builder.Services.AddScoped<IInspectionRepository, InspectionRepository>();
builder.Services.AddScoped<InspectionCommandService>();

// MonitoringDashboard DI
builder.Services.AddScoped<IHeatMapZoneRepository, HeatMapZoneRepository>();
builder.Services.AddScoped<MonitoringDashboardQueryService>();

// OrganizationAssets DI
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<OrganizationAssetsQueryService>();

// Shared Infrastructure
builder.Services.AddScoped<EventDispatcher>();

// ACL Facades
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<IHazardsContextFacade, HazardsContextFacade>();
builder.Services.AddScoped<ITechniciansContextFacade, TechniciansContextFacade>();
builder.Services.AddScoped<IRiskAssessmentsContextFacade, RiskAssessmentsContextFacade>();

var app = builder.Build();

// Localization
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(["en", "es"])
    .AddSupportedUICultures(["en", "es"]);
app.UseRequestLocalization(localizationOptions);

// Middleware pipeline
app.UseGlobalExceptionHandler();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("RiskGuardCors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Apply pending migrations before seeding or querying schema-dependent tables.
using (var migrateScope = app.Services.CreateScope())
{
    var dbContext = migrateScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = migrateScope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    // Detect if tables already exist (from a previous EnsureCreatedAsync) and
    // baseline the Initial migration so MigrateAsync() doesn't try to recreate them.
    try
    {
        var tablesExist = await dbContext.Database
            .SqlQueryRaw<int>($"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = 'access_logs'")
            .FirstAsync();

        if (tablesExist > 0)
        {
            // Create __EFMigrationsHistory table if missing
            var historyCount = await dbContext.Database
                .SqlQueryRaw<int>($"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = '__EFMigrationsHistory'")
                .FirstAsync();

            if (historyCount == 0)
            {
                await dbContext.Database.ExecuteSqlRawAsync(@"
                    CREATE TABLE `__EFMigrationsHistory` (
                        `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
                        `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
                        PRIMARY KEY (`MigrationId`)
                    )");
            }

            // Mark the Initial migration as already applied (idempotent)
            await dbContext.Database.ExecuteSqlRawAsync(
                "INSERT IGNORE INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES ('20260710082037_Initial', '8.0.11')");

            logger.LogInformation("Baseline applied: Initial migration marked as already applied (tables pre-exist).");
        }
    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "Could not check or baseline migration history; proceeding with MigrateAsync().");
    }

    // Apply any remaining pending migrations (e.g., AddAssetType)
    await dbContext.Database.MigrateAsync();
}

// Database seeding
if (app.Configuration.GetValue("SeedDatabase", true))
{
    using var scope = app.Services.CreateScope();
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();
