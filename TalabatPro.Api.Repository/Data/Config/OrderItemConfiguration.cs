using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities.OrderModule;

namespace TalabatPro.Api.Repository.Data.Config
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OI => OI.Product, OI => OI.WithOwner());
            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");

        }
    }
}
