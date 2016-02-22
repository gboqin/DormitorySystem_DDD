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
    public class DormSettingRepository:EntityFrameworkRepository<DormSetting,long>,IDormSettingRepository
    {
        public DormSettingRepository(IRepositoryContext context) : base(context) { }
    }
}
