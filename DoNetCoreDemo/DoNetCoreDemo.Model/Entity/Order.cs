using DoNetCoreDemo.Model.Database;

namespace DoNetCoreDemo.Model.Entity
{
    public class Order : BaseEntity
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        public User User { get; set; }
    }
}
