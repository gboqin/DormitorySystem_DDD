using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Repositories;
using DromitorySystem.Domain.Entities;

namespace DormitorySystem.Application.Impl
{
    public class DormSettingService : IDormSettingService
    {
        private IDormSettingRepository _setRepository;
        public DormSettingService(IDormSettingRepository setRepository)
        {
            this._setRepository = setRepository;
        }

        public OperationResult Add(DormSettingDto model)
        {
            if (ExistSetting(model))
            {
                DormSetting dormSetting = new DormSetting { set_TypeId = model.TypeId, set_Content = model.Content };
                try {
                    this._setRepository.Add(dormSetting);
                    return new OperationResult(OperationResultType.Success, "添加成功！", new DormSettingDto { Id = dormSetting.Id, TypeId = model.TypeId, TypeName = model.TypeName, Content = model.Content });
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, "添加保存失败！",e);
                }
            }
            else
            {
                return new OperationResult(OperationResultType.Error, "资料已存在！");
            }
        }

        public OperationResult Delete(DormSettingDto model)
        {
            DormSetting setting = this._setRepository.GetByKey(model.Id);
            if (setting != null)
            {
                setting.IsDeleted = true;
                try
                {
                    this._setRepository.Update(setting);
                    return new OperationResult(OperationResultType.Success, "删除成功！");
                }catch(Exception e) { return new OperationResult(OperationResultType.Error,"删除保存失败！", e); }               
            }
            return new OperationResult(OperationResultType.Error, "不能删除空记录！");
        }

        public OperationResult Edit(DormSettingDto model)
        {
            DormSetting setting = this._setRepository.GetByKey(model.Id);
            if (setting == null){
                return new OperationResult(OperationResultType.Error, "修改的记录不存！");
            }
            if (!ExistSetting(model)){
                return new OperationResult(OperationResultType.Error, "资料已存在！");
            }

            try
            {
                setting.set_TypeId = model.TypeId;
                setting.set_Content = model.Content;
                this._setRepository.Update(setting);
                return new OperationResult(OperationResultType.Success, "修改成功！");
            }catch(Exception e) { return new OperationResult(OperationResultType.Error, "修改保存失败！", e); }
        }

        public IQueryable<DormSettingDto> GetAll()
        {
            return _setRepository.GetAll().Where(ds=>ds.IsDeleted==false).Select(ds => new DormSettingDto { Id = ds.Id, TypeId = ds.set_TypeId, TypeName = ds.DormOptionType.option_Name, Content = ds.set_Content });
        }

        public DormSettingDto GetByKey(long key)
        {
            DormSetting dormSetting = _setRepository.GetByKey(key);
            return new DormSettingDto { Id=dormSetting.Id,TypeId=dormSetting.set_TypeId,TypeName=dormSetting.DormOptionType.option_Name,Content=dormSetting.set_Content};
        }

        public easyuiGridDto<DormSettingDto> GetList(string page, string rows, string content, string typeid)
        {
            int _page = int.Parse(page);
            int _rows = int.Parse(rows);
           
            IQueryable<DormSettingDto> list = GetAll().OrderBy(ds => ds.Id);
            if (!string.IsNullOrEmpty(content))
            {
                list = list.Where(ds => ds.Content.Contains(content));
            }
            if (!string.IsNullOrEmpty(typeid))
            {
                int _typeid = int.Parse(typeid);
                list = list.Where(ds => ds.TypeId == _typeid);
            }
            int? _total = list.Count();
            var data = list
                .Skip((_page - 1) * _rows)
                .Take(_rows)
                .ToList();
            return new easyuiGridDto<DormSettingDto> { total = _total, rows = data };
        }

        #region 自定义方法
        public bool ExistSetting(DormSettingDto model)
        {
            if (model.Id == 0)
            {
                return _setRepository.GetAll().Where(d => d.set_Content == model.Content && d.IsDeleted == false).Count() > 0 ? false : true;
            }
            else
            {
                return _setRepository.GetAll().Where(d => d.set_Content == model.Content && d.Id != model.Id && d.IsDeleted == false).Count() > 0 ? false : true;
            }
        }
        #endregion
    }
}
