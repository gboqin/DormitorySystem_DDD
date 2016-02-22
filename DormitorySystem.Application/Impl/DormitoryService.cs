using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Entities;
using DromitorySystem.Domain.Repositories;

namespace DormitorySystem.Application.Impl
{
    public class DormitoryService : IDormitoryService
    {
        private IDormitoryRepository _dormRepository;
        public DormitoryService(IDormitoryRepository dormRepository)
        {
            this._dormRepository = dormRepository;
        }

        #region 实现接口
        public OperationResult Add(BuildDto model)
        {
            if (ExistBuildCode(model.dorm_core) == false){
                return new OperationResult(OperationResultType.Error, "名称重复！");
            }
            Dormitory dorm = new Dormitory();
            dorm.dorm_Code = model.dorm_core;
            dorm.dorm_Level = 1;
            dorm.dorm_ParentId = null;
            if (model.dorm_levels > 0)
                dorm.dorm_State = "closed";
            else
                dorm.dorm_State = "open";
            dorm = RecursionDormGeneral(dorm, model);
            try {
                this._dormRepository.Add(dorm);
                return new OperationResult(OperationResultType.Success, "新增成功！", new DormitoryDto { id = dorm.Id, text = dorm.dorm_Code, level = dorm.dorm_Level, pid = dorm.dorm_ParentId, state = dorm.dorm_State });
            } catch(Exception e) {
                return new OperationResult(OperationResultType.Error, "新增保存失败！", e);
            }
        }

        public OperationResult Delete(DormitoryDto model)
        {
            List<Dormitory> dormitories = GetDormitoriesById(model.id);
            Dormitory parent = null;
            if (countChildren(model) == 0)
            {
                parent = this._dormRepository.GetByKey((long)model.pid);
                parent.dorm_State = "open";
            }
            try {
                this._dormRepository.UpDeleteListDate(dormitories, parent);
                return new OperationResult(OperationResultType.Success, "成功删除！");
            } catch(Exception e) {
                return new OperationResult(OperationResultType.Error, "删除失败！", e);
            }
        }

        public List<DormitoryDto> GetRootData(string Id)
        {
            if (Id != null)
            {
                int? pid = int.Parse(Id);
                return this._dormRepository.GetAll().Where(d => d.dorm_ParentId ==pid  && d.IsDeleted == false).Select(d => new DormitoryDto { id = d.Id, text = d.dorm_Code, level = d.dorm_Level, pid = d.dorm_ParentId, state = d.dorm_State }).ToList();
            }
            else
            {
                return this._dormRepository.GetAll().Where(d => Object.Equals(d.dorm_ParentId, null) && d.IsDeleted == false).Select(d => new DormitoryDto { id = d.Id, text = d.dorm_Code, level = d.dorm_Level, pid = d.dorm_ParentId, state = d.dorm_State }).ToList();
            }
        }

        public List<DormitoryDto> GetDormitories(long Id) {
            return GetDormitoriesById(Id).Select(d => new DormitoryDto { id = d.Id, text = d.dorm_Code, level = d.dorm_Level, pid = d.dorm_ParentId, state = d.dorm_State }).ToList();
        }

        #endregion

        #region 自定义私有方法
        private Dormitory RecursionDormGeneral(Dormitory parent,BuildDto build)
        {
            Dormitory _parent = parent;
            List<Dormitory> _childs = new List<Dormitory>();
            switch (_parent.dorm_Level)
            {
                case 1:
                    if (build.dorm_levels > 0) {
                        for(var x = 1; x <= build.dorm_levels; ++x)
                        {
                            Dormitory child = new Dormitory();
                            child.dorm_Code = parent.dorm_Code + "-" + x.ToString();
                            child.dorm_Level = 2;
                            child.dorm_State = "open";
                            if (build.dorm_rooms > 0){ child.dorm_State = "closed"; }
                            child.Parent = _parent;
                            child = RecursionDormGeneral(child, build);
                            _childs.Add(child);
                        }
                        _parent.ChildKeys = _childs;
                    }
                    break;
                case 2:
                    if (build.dorm_rooms > 0)
                    {
                        for(var y = 1; y <= build.dorm_rooms; ++y)
                        {
                            Dormitory child = new Dormitory();
                            child.dorm_Code = parent.dorm_Code + y.ToString().PadLeft(2, '0');
                            child.dorm_Level = 3;
                            child.dorm_State = "open";
                            if (build.dorm_bads > 0) { child.dorm_State = "closed"; }
                            child.Parent = _parent;
                            child = RecursionDormGeneral(child, build);
                            _childs.Add(child);
                        }
                        _parent.ChildKeys = _childs;
                    }
                    break;
                case 3:
                    if (build.dorm_bads > 0)
                    {
                        for(var z = 1; z <= build.dorm_bads; ++z)
                        {
                            Dormitory child = new Dormitory();
                            child.dorm_Code = parent.dorm_Code + "-" + z.ToString().PadLeft(2, '0');
                            child.dorm_Level = 4;
                            child.dorm_State = "open";
                            child.Parent = _parent;
                            _childs.Add(child);
                        }
                        _parent.ChildKeys = _childs;
                    }
                    break;
            }
            return _parent;
        }

        private bool ExistBuildCode(string code)
        {
            return this._dormRepository.GetAll().Where(d => d.dorm_Code == code && d.IsDeleted == false).Count() > 0 ? false : true;
        }

        private int countChildren(DormitoryDto model)
        {
            return this._dormRepository.GetAll().Where(d => d.dorm_ParentId == model.pid  && d.Id != model.id && d.IsDeleted == false).Count();
        }

        /// <summary>
        /// </summary>
        /// <param name="Id">父宿舍对象Id</param>
        /// <returns></returns>
        private List<Dormitory> GetChildren(long Id)
        {
            return this._dormRepository.GetAll().Where(d => d.dorm_ParentId == Id && d.IsDeleted == false).ToList();
        }

        /// <summary>
        /// 递归宿舍树
        /// </summary>
        /// <param name="dormitories"></param>
        /// <param name="sumDormitories"></param>
        /// <returns></returns>
        private List<Dormitory> RecursionDormitories(List<Dormitory> dormitories,List<Dormitory> sumDormitories)
        {
            List<Dormitory> _dormitories = dormitories;
            List<Dormitory> _sumDormitories = sumDormitories;
            foreach(var dormitory in _dormitories)
            {
                _sumDormitories.Add(dormitory);
                List<Dormitory> child = GetChildren(dormitory.Id);
                _sumDormitories = RecursionDormitories(child, _sumDormitories);
            }
            return _sumDormitories;
        }

        /// <summary>
        /// 取得宿舍树
        /// </summary>
        /// <param name="dorm"></param>
        /// <returns></returns>
        private List<Dormitory> GetDormitoriesById(long Id)
        {
            List<Dormitory> _dormitories = GetChildren(Id);
            List<Dormitory> _sumDormitories = new List<Dormitory>();
            Dormitory _dorm = this._dormRepository.GetByKey(Id);
            _sumDormitories.Add(_dorm);
            _sumDormitories = RecursionDormitories(_dormitories, _sumDormitories);
            return _sumDormitories;
        }
        #endregion
    }
}
