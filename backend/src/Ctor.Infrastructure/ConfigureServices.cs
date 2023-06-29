using System.Text;
using Ctor.Application.Auth.Interfaces;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Interfaces.Bus;
using Ctor.Application.Common.Models;
using Ctor.Domain.Repositories;
using Ctor.Infrastructure.Core;
using Ctor.Infrastructure.Persistence;
using Ctor.Infrastructure.Persistence.Repositories;
using Ctor.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Ctor.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CtorDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.Configure<MailSetting>((mailSetting) => {
            mailSetting.ApiSecret = configuration["Email:ApiSecret"];
            mailSetting.ApiKey = configuration["Email:ApiKey"];
            mailSetting.FromEmail = configuration["Email:FromEmail"];
            mailSetting.DiplayName = configuration["Email:DiplayName"];
        });
        
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IPhaseRepository, PhaseRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IProjectPhotoRepository, ProjectPhotoRepository>();
        services.AddScoped<IBuildingBlockRepository, BuildingBlockRepository>();
        services.AddScoped<ICompanyLogoRepository, CompanyLogoRepository>();
        services.AddScoped<IProjectDocumentRepository, ProjectDocumentRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IMaterialRepository, MaterialRepository>();
        services.AddScoped<IMaterialTypeRepository, MaterialTypeRepository>();
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddScoped<IRequiredMaterialRepository, RequiredMaterialRepository>();
        services.AddScoped<IVendorTypeRepository, VendorTypeRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IRequiredServiceRepository, RequiredServiceRepository>();
        services.AddScoped<IPhaseStepRepository, PhaseStepRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddTransient(typeof(Lazy<>), typeof(LazyInstance<>));
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddTransient<IPasswordService,PasswordService>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<INumberGenerateService, NumberGenerateSercice>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddTransient<ICsvFileService, CsvFileService>();
        
        services.AddScoped<IAddressParsingService, AddressParsingService>();
        services.AddScoped<IGroupsService, GroupsService>();
        services.AddScoped<ICompanyIdGeneratorService, CompanyIdGeneratorService>();

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            var filesFolderPath = Path.GetFullPath(configuration["FilesFolder"]);

            services.AddScoped<IFileManipulatorService, FileManipulatorService>(options =>
            {
                return new FileManipulatorService(
                    new FileManipulatorSettings() { FolderPath = filesFolderPath },
                    options.GetService<ILogger<FileManipulatorService>>());
            });

            services.AddTransient<IEventBus, RabbitMqBus>(sp =>
            {
                IServiceScopeFactory scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                MessageBrokerSettings settings = new(
                    configuration["MessageBroker:HostName"],
                    Convert.ToUInt16(configuration["MessageBroker:Port"]),
                    configuration["MessageBroker:UserName"],
                    configuration["MessageBroker:Password"]);

                return new RabbitMqBus(scopeFactory, settings, sp.GetService<ILogger<RabbitMqBus>>());
            });
        }
        else
        {

            services.AddScoped<IFileManipulatorService, AzureStorageService>(options =>
            {
                return new AzureStorageService(configuration, options.GetService<ILogger<AzureStorageService>>());
            });

            services.AddTransient<IEventBus, AzureServiceBus>(options =>
            {
                var scopeFactory = options.GetService<IServiceScopeFactory>();
                return new AzureServiceBus(scopeFactory, configuration, options.GetService<ILogger<AzureServiceBus>>());
            });
        }

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
        {
            var secret = configuration["Jwt:Secret"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            };
        });

        return services;
    }
}
