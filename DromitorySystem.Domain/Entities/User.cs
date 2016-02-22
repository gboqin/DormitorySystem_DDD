using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    [Table("tb_User")]
    public class User:AggregateRoot<long>
    {
        public string usr_Name { get; set; }
        public string usr_Code { get; set; }
        public string usr_Password { get; set; }
        [ForeignKey("Level")]
        public long usr_lev_Id { get; set; }
        public virtual Level Level { get; set; }
    }
}
