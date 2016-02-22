using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Repositories
{
    public interface IDepartmentRepository:IRepository<Department,long>
    {
        /// <summary>
        /// Department自定义方法
        /// </summary>
        /// <param name="departments"></param>
        void UpDeleteListDate(List<Department> departments, Department parendDept);
    }
}
