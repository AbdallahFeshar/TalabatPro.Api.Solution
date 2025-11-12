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
    internal class CategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired();
        }
    }
}
