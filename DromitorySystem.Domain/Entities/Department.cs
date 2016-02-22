using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    [Table("tb_Department")]
    public class Department:AggregateRoot<long>
    {
        [Required]
        public long? Dept_ParentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Dept_Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Dept_State { get; set; }

        public string Dept_Order { get; set; }
    }
}
