using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    [Table("tb_Dormitory")]
    public class Dormitory:AggregateRoot<long>
    {
        [Required]
        [StringLength(50)]
        public string dorm_Code { get; set; }
        public long? dorm_ParentId { get; set; }
        /// <summary>
        /// 层次()
        /// </summary>
        [Required]
        public long dorm_Level { get; set; }
        /// <summary>
        /// easyui "open"or"closed"
        /// </summary>
        [Required]
        [StringLength(20)]
        public string dorm_State { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public virtual Dormitory Parent{ get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        [ForeignKey("dorm_ParentId")]
        public virtual ICollection<Dormitory> ChildKeys { get; set; }

        public virtual ICollection<DormSetRelation> DormSetRelations { get; set; }
    }
}
