using DormitorySystem.Repositories.EntityFramework;
using DromitorySystem.Domain.Entities;
using DromitorySystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Repositories
{
    public class GoodsRepository : EntityFrameworkRepository<Goods, long>, IGoodsRepository
    {
        public GoodsRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
