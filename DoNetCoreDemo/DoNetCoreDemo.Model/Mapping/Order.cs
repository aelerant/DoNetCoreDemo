using DoNetCoreDemo.Model.Database;
using DoNetCoreDemo.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ReSharper disable once CheckNamespace
namespace DoNetCoreDemo.Model.Mapping
{
    public class OrderMap : EntityBaseConfiguration<Order>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Order> builder)
        {
            //Primary Key
            builder.HasKey(b => new { b.Id });
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.Price).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,3)");

            builder.HasOne(b => b.User).WithMany().HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Cascade);
            //Microsoft.EntityFrameworkCore.Relational
            builder.ToTable("Order");
        }
    }
}

