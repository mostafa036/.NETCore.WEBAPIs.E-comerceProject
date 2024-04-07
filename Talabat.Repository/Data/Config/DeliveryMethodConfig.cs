using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
    public class DeliveryMethodConfig : IEntityTypeConfiguration<Deliverymethod>
    {
        public void Configure(EntityTypeBuilder<Deliverymethod> builder)
        {
            builder.Property(Dmethod => Dmethod.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
