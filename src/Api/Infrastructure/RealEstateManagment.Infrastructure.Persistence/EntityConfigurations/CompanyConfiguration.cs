using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateManagment.Api.Domain.Models;
using RealEstateManagment.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagment.Infrastructure.Persistence.EntityConfigurations;

public class CompanyConfiguration : BaseEntityConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);


        builder.ToTable("company", RealEstateManagmentDbContext.DEFAULT_SCHEMA);

        //builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
        //builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
        //builder.Property(x => x.PostCode).IsRequired();
        //builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        //builder.Property(x => x.FaxNumber).HasMaxLength(20);

    }
}