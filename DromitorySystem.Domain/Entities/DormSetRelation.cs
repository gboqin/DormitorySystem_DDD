using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    //宿舍属性设置
    [Table("tb_DormSetRelation")]
    public class DormSetRelation:AggregateRoot<long>
    {
        [Required]
        public long dsr_DormId { get; set; }
        [Required]
        public long dsr_DormSetId { get; set; }
        public bool dsr_Private { get; set; }
        public bool dsr_State { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime? dsr_Enable { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime? dsr_unEnable { get; set; }
        [ForeignKey("dsr_DormId")]
        public virtual Dormitory Dormitory { get; set; }
        [ForeignKey("dsr_DormSetId")]
        public virtual DormSetting DormSetting { get; set; }
    }
}
