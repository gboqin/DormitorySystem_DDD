using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Repositories
{
    public interface IDormitoryRepository:IRepository<Dormitory,long>
    {
        /// <summary>
        /// Dormitory自定义方法
        /// </summary>
        /// <param name="dormitories"></param>
        void UpDeleteListDate(List<Dormitory> dormitories, Dormitory dormitory);
    }
}
