using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoNetCoreDemo.Model.Database
{
    // 基础实体配置抽象类
    public abstract class EntityBaseConfiguration<TEntity>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity  // 约束必须继承自BaseEntity
    {
        // 实现接口方法（密封防止子类意外重写）
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // 通用配置（适用于所有实体）
            //builder.HasKey(x => x.Id);          // 主键配置
            //builder.Property(x => x.Id)         // ID字段配置
            //    .ValueGeneratedOnAdd();         // 自增策略

            // 软删除配置（假设BaseEntity有IsDeleted字段）
            builder.HasQueryFilter(x => !x.IsDeleted);

            // 调用子类具体配置
            ConfigureDerived(builder);
        }

        // 抽象方法要求子类必须实现具体配置
        protected abstract void ConfigureDerived(EntityTypeBuilder<TEntity> builder);
    }

    // 基础实体定义示例
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    }


    //public abstract class BaseContext : DbContext
    //{
    //    // 允许派生类通过依赖注入配置选项
    //    protected BaseContext(DbContextOptions options)
    //        : base(options)
    //    {
    //    }

    //    // 为迁移工具保留的无参构造（EF Core推荐使用设计时工厂替代）
    //    protected BaseContext()
    //    {
    //    }

    //    // 提供模型配置扩展点（当前仅调用基类方法）
    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);
    //    }
    //}
}