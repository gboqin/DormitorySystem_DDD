using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class DormSetRelationDto
    {
        public long Id { get; set; }
        public long dsr_DormId { get; set; }
        public long dsr_SetTypeId { get; set; }
        public string SetType { get; set; }
        public long dsr_DormSetId { get; set; }
        public string SetContent { get; set; }
        public bool? dsr_Private { get; set; }
        public bool? dsr_Cover { get; set; }
        public bool dsr_State { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime? dsr_Enable { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime? dsr_unEnable { get; set; }
    }
}
