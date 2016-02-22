using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Entities
{
    public abstract class Entity<TKey>:IEntity<TKey>
    {
        public Entity()
        {
            this.AddTime = System.DateTime.Now;
            this.IsDeleted = false;
        }
        [Key]
        public TKey Id { get; set; }
        public DateTime AddTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
