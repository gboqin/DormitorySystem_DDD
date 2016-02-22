using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class BuildDto
    {
        public string dorm_core { get; set; }
        public int? dorm_levels { get; set; }
        public int? dorm_rooms { get; set; }
        public int? dorm_bads { get; set; }
    }
}
