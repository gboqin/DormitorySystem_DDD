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
    public class DepartmentRepository:EntityFrameworkRepository<Department,long>,IDepartmentRepository
    {
        public DepartmentRepository(IRepositoryContext context) : base(context) { }


        public void UpDeleteListDate(List<Department> departments, Department parendDept)
        {
            if (parendDept != null)
            {
                EfContext.RegisterModified<Department, long>(parendDept);
            }          

            foreach (Department item in departments)
            {
                item.IsDeleted = true;
                //item.Dept_State = item.Dept_State == "close" ? "open" : item.Dept_State;
                EfContext.RegisterModified<Department,long>(item);
            }
            EfContext.Commit();
        }
    }
}
