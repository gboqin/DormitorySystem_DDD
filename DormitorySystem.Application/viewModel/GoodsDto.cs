using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class GoodsDto
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Decription { get; set; }
    }
}
