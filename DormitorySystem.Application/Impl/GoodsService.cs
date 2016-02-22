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
    public class GoodsService : IGoodsService
    {
        #region 私有属性与构造函数
        private IGoodsRepository _goodsRepository;
        public GoodsService(IGoodsRepository goodsRepository)
        {
            this._goodsRepository = goodsRepository;
        }
        #endregion

        #region 实现接口方法
        public OperationResult Add(GoodsDto model)
        {
            if (model == null) {
                return new OperationResult(OperationResultType.Error, "不能添加空记录！",null);
            }
            Goods goods = new Goods { Id = model.Id, Name = model.Name, Spec = model.Spec, Decription = model.Decription };
            try {
                this._goodsRepository.Add(goods);
                return new OperationResult(OperationResultType.Success, "添加成功！", new GoodsDto { Id = goods.Id, Name = goods.Name, Spec = goods.Spec, Decription = goods.Decription });
            }
            catch(Exception e)
            {
                return new OperationResult(OperationResultType.Error, "添加保存失败！", e);
            }
        }

        public OperationResult Delete(GoodsDto model)
        {
            if (model == null) {
                return new OperationResult(OperationResultType.Error, "不能删除空记录！");
            }
            Goods goods = this._goodsRepository.GetByKey(model.Id);
            if (goods != null)
            {
                try {
                    goods.IsDeleted = true;
                    this._goodsRepository.Update(goods);
                    return new OperationResult(OperationResultType.Success, "成功删除记录！");
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, "删除记录失败！",e);
                }
            }
            return new OperationResult(OperationResultType.Error, "找不到将要删除的记录！");
        }

        public OperationResult Edit(GoodsDto model)
        {
            if (model == null)
            {
                return new OperationResult(OperationResultType.Error, "不能修改空记录！");
            }
            Goods goods = this._goodsRepository.GetByKey(model.Id);
            if (goods != null)
            {
                try
                {
                    goods.Name = model.Name;
                    goods.Spec = model.Spec;
                    goods.Decription = model.Decription;
                    this._goodsRepository.Update(goods);
                    return new OperationResult(OperationResultType.Success, "成功修改记录！");
                }
                catch (Exception e)
                {
                    return new OperationResult(OperationResultType.Error, "修改记录失败！",e);
                }
            }
            return new OperationResult(OperationResultType.Error, "找不到将要修改的记录！");
        }

        public easyuiGridDto<GoodsDto> GetAll(string page, string pagerows, string name)
        {
            int _page = int.Parse(page);
            int _rows = int.Parse(pagerows);
            //报错，未将对象引用设置到对象的实例
            //IQueryable<GoodsDto> list = GetAll().Where(g => (!string.IsNullOrEmpty(name) ? g.Name == name : true)).OrderBy(g => g.Id);
            IQueryable<GoodsDto> list = GetAll().OrderBy(g => g.Id);
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(g => g.Name == name);
            }
            int? _total = list.Count();
            var data = list
                .Skip((_page - 1) * _rows)
                .Take(_rows)            
                .ToList();
            return new easyuiGridDto<GoodsDto> { total = _total, rows = data};
        }

        public GoodsDto GetByKey(long key)
        {
            Goods goods=this._goodsRepository.GetByKey(key);
            if (goods!=null) {
                return new GoodsDto { Id = goods.Id, Name = goods.Name, Spec = goods.Spec, Decription = goods.Decription };
            }
            return null;
        }

        public IQueryable<GoodsDto> GetAll()
        {
            return this._goodsRepository.GetAll().Where(g => g.IsDeleted == false).Select(g => new GoodsDto { Id = g.Id, Name = g.Name, Spec = g.Spec, Decription = g.Decription });
        }
        #endregion
    }
}
