using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class DormSettingDto
    {
        public long Id { get; set; }
        [Required]
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        [Required]
        [StringLength(200)]
        public string Content { get; set; }
    }
}
