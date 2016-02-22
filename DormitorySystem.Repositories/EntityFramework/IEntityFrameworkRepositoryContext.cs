using DromitorySystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Repositories.EntityFramework
{
    public interface IEntityFrameworkRepositoryContext:IRepositoryContext
    {
        DromityDbContext DbContext { get; }
    }
}
