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
    public class DormSetRelationRepository: EntityFrameworkRepository<DormSetRelation,long>, IDormSetRelationRepository
    {
        public DormSetRelationRepository(IRepositoryContext context) : base(context) { }

        public void AddListDate(List<DormSetRelation> dormsetrelations)
        {
            foreach(DormSetRelation item in dormsetrelations)
            {
                EfContext.RegisterNew<DormSetRelation, long>(item);
            }
            EfContext.Commit();
        }

        public void UpDeleteList(List<DormSetRelation> dormsetrelations)
        {
            foreach (DormSetRelation item in dormsetrelations)
            {
                item.IsDeleted = true;
                EfContext.RegisterModified<DormSetRelation, long>(item);
            }
            EfContext.Commit();
        }

        public void UpListDate(List<DormSetRelation> dormsetrelations, DormSetRelation dormsetrelation)
        {
            if (dormsetrelation != null)
            {
                EfContext.RegisterModified<DormSetRelation, long>(dormsetrelation);
            }

            foreach (DormSetRelation item in dormsetrelations)
            {
                item.dsr_State = false;
                item.dsr_unEnable = DateTime.Parse(DateTime.Now.ToShortDateString());
                EfContext.RegisterModified<DormSetRelation, long>(item);
            }
            EfContext.Commit();
        }
    }
}
