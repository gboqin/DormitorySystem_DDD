using DormitorySystem.Repositories.EntityFramework;
using DromitorySystem.Domain.Repositories;
using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Repositories
{
    public class DormOptiontypeRepository: EntityFrameworkRepository<DormOptionType, long>, IDormOptiontypeRepository
    {
        public DormOptiontypeRepository(IRepositoryContext context) : base(context) { }
    }
}
