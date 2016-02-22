using DromitorySystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Entities;

namespace DormitorySystem.Application.Impl
{
    public class DepartmentService:IDepartmentService
    {
        private IDepartmentRepository _deptRepository;
        public DepartmentService(IDepartmentRepository deptRepository) {
            this._deptRepository = deptRepository;
        }
        #region 实现接口功能
        public OperationResult Add(DepartmentDto model)
        {
            if (ExistChildren(model))
            {
                Department department = new Department { Dept_Name = model.text, Dept_ParentId = (model.pid == null ? 0 : model.pid), Dept_State = "open" };
                this._deptRepository.Add(department);
                if (model.pid != null)
                {
                    Department pdepartment = GetByKey(model.pid == null ? 0 : (int)model.pid);
                    if (pdepartment != null && pdepartment.Dept_State == "open")
                    {
                        pdepartment.Dept_State = "closed";
                    }
                    this._deptRepository.Update(pdepartment);
                }
                
                return new OperationResult(OperationResultType.Success, "添加成功！", new DepartmentDto { id=department.Id,text=model.text,pid=model.pid,order=model.order});
            }
            return new OperationResult(OperationResultType.Error, "添加失败！");
        }

        public OperationResult Update(DepartmentDto model)
        {
            if (ExistChildren(model))
            {
                try { 
                    Department department = GetByKey(model.id);
                    department.Dept_Name = model.text;
                    if (model.order != null)
                    {
                        department.Dept_Order = model.order;
                    }                   
                    this._deptRepository.Update(department);
                    return new OperationResult(OperationResultType.Success, "修改成功！", new DepartmentDto { id = department.Id, text = model.text, pid = model.pid, order = model.order });
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, "修改保存失败！", e);
                }
            }
            return new OperationResult(OperationResultType.Error, "修改失败！");
        }

        /// <summary>
        /// 取消不用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OperationResult UpDeletedate(long id)
        {
            List<Department> departments = new List<Department>();
            List<Department> edepartments = new List<Department>();
            Department department = new Department();

            department = GetByKey(id);
            departments.Add(department);
            departments = GetListById(id, departments);
            Department pdept = null;
            if (department.Dept_ParentId != 0)
            {
                if (GetChildren((long)department.Dept_ParentId).Count() == 1)
                {
                    pdept = GetByKey((long)department.Dept_ParentId);
                    pdept.Dept_State = "open";
                }
            }
            try {
                this._deptRepository.UpDeleteListDate(departments, pdept);
                return new OperationResult(OperationResultType.Success, "删除成功！");
            }
            catch(Exception e)
            {
                return new OperationResult(OperationResultType.Error, "删除失败！",e);
            }
            
        }

        public OperationResult UpDltdate(TreeDto tree)
        {
            List<Department> departments = new List<Department>();
            List<Department> edepartments = new List<Department>();
            Department department = new Department();

            department = GetByKey(tree.id);
            departments.Add(department);
            departments = treetolist(tree, departments);
            Department pdept = null;
            if (department.Dept_ParentId != 0)
            {
                if (GetChildren((long)department.Dept_ParentId).Count() == 1)
                {
                    pdept = GetByKey((long)department.Dept_ParentId);
                    pdept.Dept_State = "open";
                }
            }
            try
            {
                this._deptRepository.UpDeleteListDate(departments, pdept);
                return new OperationResult(OperationResultType.Success, "删除成功！");
            }
            catch (Exception e)
            {
                return new OperationResult(OperationResultType.Error, "删除失败！",e);
            }

        }

        public List<TreeDto> GetAllTree()
        {
            var _root = GetChildren((long)0).Select(d => new TreeDto { id = d.Id, text = d.Dept_Name, state = d.Dept_State }).ToList();
            if (_root != null)
            {
                for (int i = 0; i < _root.Count(); i++)
                {
                    _root[i] = RecursionTreeGeneral(_root[i]);
                }
            }
            return _root;
        }

        public DepartmentDto GetDeptByKey(long key)
        {
            Department dept = _deptRepository.GetByKey(key);
            return dept == null ? null : new DepartmentDto { id=dept.Id,text=dept.Dept_Name,pid=dept.Dept_ParentId,order=dept.Dept_Order};
        }
        #endregion

        #region 自定义进程
        private bool ExistChildren(DepartmentDto model)
        {
            if (model.id == 0)
            {
                return  _deptRepository.GetAll().Where(d => d.Dept_Name == model.text && d.IsDeleted == false).Count() > 0 ? false : true;
            }
            else
            {
                return  _deptRepository.GetAll().Where(d => d.Dept_Name == model.text && d.Id != model.id && d.IsDeleted == false).Count() > 0 ? false : true;
            }
        }

        private Department GetByKey(long key)
        {
             return _deptRepository.GetByKey(key);
        }

        private IQueryable<Department> GetChildren(long id)
        {
            return _deptRepository.GetAll().Where(d => d.Dept_ParentId == id && d.IsDeleted == false).OrderBy(d=>d.Dept_Order);
        }

        private TreeDto RecursionTreeGeneral(TreeDto tree)
        {
            var treechildren = GetChildren(tree.id).Select(d=>new TreeDto { id=d.Id,text=d.Dept_Name,state=d.Dept_State }).ToList();
            if (treechildren != null)
            {
                for (int i = 0; i < treechildren.Count(); i++)
                {
                    treechildren[i] = RecursionTreeGeneral(treechildren[i]);
                }
                tree.children = treechildren;
            }
            return tree;
        }

        private List<Department> GetListById(long id,List<Department> departments)
        {
            var deptchildren = GetChildren(id).ToList();
            List<Department> _departments = new List<Department>();
            _departments = departments;
            if (deptchildren != null)
            {
                foreach (Department itree in deptchildren)
                {
                    var _department = GetByKey(itree.Id);
                    _departments.Add(_department);
                    _departments = GetListById(itree.Id, _departments);
                }
            }
            return _departments;
        }

        private List<Department> treetolist(TreeDto tree, List<Department> departments)
        {
            List<Department> _departments = new List<Department>();
            _departments = departments;
            if (tree.children != null)
            {
                foreach (TreeDto itree in tree.children)
                {
                    var _department = GetByKey(itree.id);
                    _departments.Add(_department);
                    _departments = treetolist(itree, _departments);
                }
            }
            return _departments;
        }
        #endregion
    }
}
