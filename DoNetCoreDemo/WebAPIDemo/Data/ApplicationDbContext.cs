using DoNetCoreDemo.Model.Database;
using DoNetCoreDemo.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDemo.Data
{
    public class ApplicationDbContext : DbContext, IContextDbSet
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //1.不需要Mapping
            //builder.Entity<User>()
            //    .ToTable("User") // 表名映射
            //    .HasKey(p => p.Id); // 主键配置

            //builder.Entity<User>()
            //    .Property(p => p.Name)
            //    .IsRequired()
            //    .HasMaxLength(100);

            base.OnModelCreating(builder);
            ContextMap.AddMap(builder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}