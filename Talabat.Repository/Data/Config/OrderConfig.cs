﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.Shipingaddress, NP =>
            {

                NP.WithOwner();

            });
            builder.Property(O => O.Status)
                .HasConversion(

                OStatus => OStatus.ToString(),
                OStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), OStatus)

                );
            builder.HasMany( O=> O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(O=>O.Subtotal).HasColumnType("decimal(18,2)");
        }
    }
}
