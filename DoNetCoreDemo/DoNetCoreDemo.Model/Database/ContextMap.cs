using DoNetCoreDemo.Model.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DoNetCoreDemo.Model.Database
{
    public class ContextMap
    {
        public static void AddMap(ModelBuilder builder)
        {
            ////配置实体类型
            //builder.Entity<InventorieView1>(entity =>
            //{
            //    entity.HasNoKey();
            //    entity.ToView("InventorieView1");
            //    entity.Property(b => b.Number).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,3)");

            //});

            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new OrderMap());
        }
    }
}