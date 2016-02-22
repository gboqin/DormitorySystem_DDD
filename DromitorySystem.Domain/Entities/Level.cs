using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    [Table("tb_Level")]
    public class Level:AggregateRoot<long>
    {
        [Required]
        [StringLength(50,ErrorMessage ="不能超过50个字符！")]
        public string lev_Text { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
