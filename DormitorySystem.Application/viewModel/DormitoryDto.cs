using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class DormitoryDto
    {
        public long id { get; set; }
        public string text { get; set; }
        public long? pid { get; set; }
        public long? level { get; set; }
        public string state { get; set; }
    }
}
