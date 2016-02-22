using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Repositories
{
    public interface IDormSetRelationRepository :IRepository<DormSetRelation,long>
    {
        void UpDeleteList(List<DormSetRelation> dormsetrelations);
        void UpListDate(List<DormSetRelation> dormsetrelations, DormSetRelation dormsetrelation);
        void AddListDate(List<DormSetRelation> dormsetrelations);
    }
}
