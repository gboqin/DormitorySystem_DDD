using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class DepartmentDto
    {
        public long id { get; set; }
        [Required]
        public string text { get; set; }
        public long? pid { get; set; }
        public string order { get; set; }
    }
}
