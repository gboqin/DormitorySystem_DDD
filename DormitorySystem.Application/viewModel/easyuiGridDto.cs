using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class easyuiGridDto<TDto>
    {
        public int? total { get; set; }
        public List<TDto> rows { get; set; }
    }
}
