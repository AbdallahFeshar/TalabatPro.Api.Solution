using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities;

namespace TalabatPro.Api.Repository.Data.Config
{
    internal class BrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(b => b.Name)
                   .IsRequired();
        }
    }
}
