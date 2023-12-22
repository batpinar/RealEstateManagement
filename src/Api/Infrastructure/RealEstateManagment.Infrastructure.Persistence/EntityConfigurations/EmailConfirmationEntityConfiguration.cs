using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateManagment.Api.Domain.Models;
using RealEstateManagment.Infrastructure.Persistence.Context;

namespace RealEstateManagment.Infrastructure.Persistence.EntityConfigurations;

public class EmailConfirmationEntityConfiguraition : BaseEntityConfiguration<EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.ToTable("emailconfirmation", RealEstateManagmentDbContext.DEFAULT_SCHEMA);
    }
}
