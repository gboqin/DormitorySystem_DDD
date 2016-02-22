using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    [Table("tb_Goods")]
    public class Goods:AggregateRoot<long>
    {
        [Required]
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Decription { get; set; }
    }
}
