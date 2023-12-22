using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateManagment.Infrastructure.Persistence.Context;

namespace RealEstateManagment.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RealEstateManagmentDbContext>(conf =>
        {
            var connStr = configuration["RealEstateManagementDbConnectionString"].ToString();
            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        var seedData = new SeedData();
        seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<IEntryRepository, EntryRepository>();
        //services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
        //services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();

        return services;
    }
}