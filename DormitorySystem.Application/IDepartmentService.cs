using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface IDepartmentService
    {
        OperationResult Add(DepartmentDto model);
        OperationResult Update(DepartmentDto model);
        OperationResult UpDeletedate(long id);
        OperationResult UpDltdate(TreeDto tree);
        List<TreeDto> GetAllTree();
        DepartmentDto GetDeptByKey(long key);
        //void Delete(DepartmentDto model);
        //IQueryable<Department> GetAll();
        //Department GetByKey(int key);
        //bool ExistDeptName(int Id, string deptName);
        //bool ExistChildren(int pid, int id);
    }
}
