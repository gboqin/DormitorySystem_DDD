using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    //宿舍字典类别
    [Table("tb_DormOptiontype")]
    public class DormOptionType : AggregateRoot<long>
    {
        [Required]
        [StringLength(50)]
        public string option_Name { get; set; }
        public string option_Decription { get; set; }
        public virtual ICollection<DormSetting> DormSettings { get; set; }
    }
}
