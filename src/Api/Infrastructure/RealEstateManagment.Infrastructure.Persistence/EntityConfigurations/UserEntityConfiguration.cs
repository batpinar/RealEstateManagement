using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateManagment.Api.Domain.Models;
using RealEstateManagment.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagment.Infrastructure.Persistence.EntityConfigurations
{
    public class UserEntityConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("user", RealEstateManagmentDbContext.DEFAULT_SCHEMA);

            builder.HasOne(ca => ca.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(ca => ca.CompanyId)
                            .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
