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
    public class DormitoryRepository:EntityFrameworkRepository<Dormitory,long>,IDormitoryRepository
    {
        public DormitoryRepository(IRepositoryContext context) : base(context) { }

        public void UpDeleteListDate(List<Dormitory> dormitories, Dormitory dormitory)
        {
            if (dormitory != null)
            {
                EfContext.RegisterModified<Dormitory, long>(dormitory);
            }

            foreach (Dormitory item in dormitories)
            {
                item.IsDeleted = true;
                //item.dorm_State = item.dorm_State == "close" ? "open" : item.dorm_State;
                EfContext.RegisterModified<Dormitory, long>(item);
            }
            EfContext.Commit();
        }
    }
}
