using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class MessageDto<TEntity>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public TEntity model { get; set; }
    }
}
