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
    public class UserPropertyEntityConfiguration : BaseEntityConfiguration<UserProperty>
    {
        public override void Configure(EntityTypeBuilder<UserProperty> builder)
        {
            base.Configure(builder);
            builder.ToTable("userproperty", RealEstateManagmentDbContext.DEFAULT_SCHEMA);

            builder.HasOne(ca => ca.User)
            .WithMany(c => c.UserProperties)
            .HasForeignKey(ca => ca.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

        }
    }
}