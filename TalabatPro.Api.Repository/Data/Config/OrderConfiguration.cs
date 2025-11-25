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
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, shipingAddress => shipingAddress.WithOwner());
            builder.Property(o => o.Status).HasConversion(o => o.ToString(), o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            ///builder.HasOne(o => o.DeliveryMethod)
            ///    .WithMany()
            ///    .HasForeignKey(o => o.DeliveryMethodId)
            ///    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
