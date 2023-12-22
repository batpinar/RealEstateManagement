using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RealEstateManagment.Api.Domain.Models;
using RealEstateManagment.Common.Infrastructure;
using RealEstateManagment.Common.Models;

namespace RealEstateManagment.Infrastructure.Persistence.Context;

internal class SeedData
{
    private static List<Company> GetCompany()
    {
        var result = new Faker<Company>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate,
                        i => i.Date.Between(DateTime.Now.AddYears(-50), DateTime.Now))
                .RuleFor(i => i.CompanyName, i => i.Company.CompanyName())
                .RuleFor(i => i.PhoneNumber, i => i.Phone.PhoneNumber())
                .RuleFor(i => i.FaxNumber, i => i.Phone.PhoneNumber())
                .RuleFor(i => i.PostCode, i => i.Random.Number(10000,100000))
                .RuleFor(i => i.Address, i => i.Address.FullAddress())
            .Generate(10);

        return result;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseSqlServer(configuration["RealEstateManagementDbConnectionString"]);

        var context = new RealEstateManagmentDbContext(dbContextBuilder.Options);

        if (context.Companies.Any())
        {
            await Task.CompletedTask;
            return;
        }

        var company = GetCompany();
        var companyIds = company.Select(i => i.Id);

        await context.Companies.AddRangeAsync(company);

        var guids = Enumerable.Range(0, 200).Select(i => Guid.NewGuid()).ToList();
        int counter = 0;

        var user = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => guids[counter++])
                .RuleFor(i => i.CreateDate,
                            i => i.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
                .RuleFor(i => i.HomeNumber, i => i.Phone.PhoneNumber())
                .RuleFor(i => i.PhoneNumber, i => i.Phone.PhoneNumber())
                .RuleFor(i => i.UserName, i => i.Internet.UserName())
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                .RuleFor(i => i.CompanyId, i => i.PickRandom(companyIds))
                .RuleFor(i => i.Address, i => i.Address.FullAddress())
                .RuleFor(i => i.Password, i => PasswordEncryptor.Encrpt(i.Internet.Password()))
            .Generate(200);

        await context.Users.AddRangeAsync(user);

        var propertyType = new[] { "House", "Apartment", "Condo", "Land", "Commercial" };
        var warmingType = new[] { "Radiant Heating", "Steam Radiator", "Electric Heating", "Geothermal Heating", "Solar Heating" };

        var userProperties = new Faker<UserProperty>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate,
                            i => i.Date.Between(DateTime.Now.AddDays(-300), DateTime.Now))
                .RuleFor(i => i.PropertyType, i => i.PickRandom(propertyType))
                .RuleFor(i => i.SquareMeters, i => i.Random.Number(90, 180))
                .RuleFor(i => i.NumberOfRooms, i => i.Random.Number(1, 5))
                .RuleFor(i => i.BuildingFloor, i => i.Random.Number(-2, 30))
                .RuleFor(i => i.WarmingType, i => i.PickRandom(warmingType))
                .Generate(1500);

        await context.UserProperties.AddRangeAsync(userProperties);

        await context.SaveChangesAsync();
    }

}