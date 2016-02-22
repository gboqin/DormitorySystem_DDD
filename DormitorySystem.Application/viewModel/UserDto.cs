using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class UserDto
    {
        public long Id { get; set; }
        public string usr_Name { get; set; }
        public string usr_Code { get; set; }
        public string usr_Password { get; set; }
        public long usr_lev_Id { get; set; }
        public string usr_Level{ get; set; }
    }
}
