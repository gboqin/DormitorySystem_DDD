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
    public class DormOptiontypeService : IDormOptiontypeService
    {
        private IDormOptiontypeRepository _dotRepository;
        public DormOptiontypeService(IDormOptiontypeRepository dotRepository)
        {
            this._dotRepository = dotRepository;
        }

        public OperationResult Add(DormOptiontypeDto model)
        {
            if (model == null) { return new OperationResult(OperationResultType.Error, "不能添加空值！", null); }
            DormOptionType type = new DormOptionType { Id = model.Id, option_Name = model.Name, option_Decription = model.Decription };
            try {
                _dotRepository.Add(type);
                return new OperationResult(OperationResultType.Success, "新增成功！", new DormOptiontypeDto { Id = type.Id, Name = type.option_Name, Decription = type.option_Decription });
            }
            catch (Exception e){
                return new OperationResult(OperationResultType.Error, "新增保存失败！", e);
            }
        }

        public OperationResult Delete(DormOptiontypeDto model)
        {
            if (model == null) { return new OperationResult(OperationResultType.Error, "不能删除空值！"); }
            DormOptionType type = _dotRepository.GetByKey(model.Id);
            type.IsDeleted = true;
            try{
                _dotRepository.Update(type);
                return new OperationResult(OperationResultType.Success, "删除成功！");
            }catch(Exception e)
            {
                return new OperationResult(OperationResultType.Error, "删除保存失败！", e);
            }
        }

        public OperationResult Edit(DormOptiontypeDto model)
        {
            if (model == null) { return new OperationResult(OperationResultType.Error, "不能修改空值！"); }
            DormOptionType type = _dotRepository.GetByKey(model.Id);
            type.option_Name = model.Name;
            type.option_Decription = model.Decription;
            try
            {
                _dotRepository.Update(type);
                return new OperationResult(OperationResultType.Success, "修改成功！");
            }
            catch (Exception e)
            {
                return new OperationResult(OperationResultType.Error, "修改保存失败！", e);
            }
        }

        public IQueryable<DormOptiontypeDto> GetAll()
        {
            return _dotRepository.GetAll().Where(t=>t.IsDeleted==false).Select(t=>new DormOptiontypeDto { Id=t.Id,Name=t.option_Name,Decription=t.option_Decription});
        }

        public easyuiGridDto<DormOptiontypeDto> GetAll(string page, string pagerows, string name)
        {
            int _page = int.Parse(page);
            int _rows = Convert.ToInt16(pagerows);
            //var list = _dotRepository.GetAll().Where(t => ((!string.IsNullOrEmpty(name)) ? t.option_Name == name : true) && t.IsDeleted == false).Select(t => new DormOptiontypeDto { Id = t.Id, Name = t.option_Name, Decription = t.option_Decription }).OrderBy(t=>t.Id);
            IQueryable<DormOptiontypeDto> list = GetAll().OrderBy(t => t.Id);;
            if (!string.IsNullOrEmpty(name)){ list = list.Where(t => t.Name == name); }
            int? total = list.Count();
            var data = list.Skip((_page - 1) * _rows).Take(_rows).ToList();
            return new easyuiGridDto<DormOptiontypeDto> { total = total, rows = data };
        }

        public DormOptiontypeDto GetByKey(long key)
        {
            DormOptionType type = this._dotRepository.GetByKey(key);
            if (type != null)
            {
                return new DormOptiontypeDto { Id = type.Id, Name = type.option_Name, Decription = type.option_Decription };
            }
            return null;
        }
    }
}
