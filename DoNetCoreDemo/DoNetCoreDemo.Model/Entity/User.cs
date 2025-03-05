using DoNetCoreDemo.Model.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoNetCoreDemo.Model.Entity
{
    public class User : BaseEntity
    {
        [Key] // 主键映射
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("Email")] // 列名映射
        public string Email { get; set; }
    }
}
