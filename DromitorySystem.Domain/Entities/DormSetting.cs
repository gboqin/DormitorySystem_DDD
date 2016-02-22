using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    //宿舍设置字典
    [Table("tb_DormSetting")]
    public class DormSetting : AggregateRoot<long>
    {
        [Required]
        public long set_TypeId { get; set; }
        [Required]
        [StringLength(200)]
        public string set_Content { get; set; }
        [ForeignKey("set_TypeId")]
        public virtual DormOptionType DormOptionType { get; set; }
        public virtual ICollection<DormSetRelation> DormSetRelations { get; set; }
    }
}
