using DoNetCoreDemo.Model.Database;
using DoNetCoreDemo.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ReSharper disable once CheckNamespace
namespace DoNetCoreDemo.Model.Mapping
{
    public class UserMap : EntityBaseConfiguration<User>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<User> builder)
        {
            //Primary Key
            builder.HasKey(b => new { b.Id });

            //Microsoft.EntityFrameworkCore.Relational
            builder.ToTable("User");
        }
    }
}

