using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    [Table("tb_DormDeductnoney")]
    public class DormDeductmoney:AggregateRoot<long>
    {
        [Required]
        public int ddm_DormId { get; set; }
        [Required]
        [StringLength(100)]
        public string ddm_OptionName { get; set; }
        public decimal ddm_Money { get; set; }
        [DisplayFormat(DataFormatString ="{0:d}",ApplyFormatInEditMode =true)]
        public DateTime ddm_DeductDate { get; set; }
        public string ddm_Remarks { get; set; }
    }
}
