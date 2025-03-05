using DoNetCoreDemo.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace DoNetCoreDemo.Model.Database
{
    public interface IContextDbSet
    {
        //DbSet 是 Entity Framework Core 中用于访问和操作数据库表的泛型集合。

        DbSet<User> User { get; set; }
        DbSet<Order> Order { get; set; }
    }
}