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
    public class DormSetRelationService : IDormSetRelationService
    {
        private IDormSetRelationRepository _dsrRepository;
        public DormSetRelationService(IDormSetRelationRepository dsrRepository)
        {
            _dsrRepository = dsrRepository;
        }

        public OperationResult Add(DormSetRelationDto model)
        {
            if (model == null) { return new OperationResult(OperationResultType.Error, "添加内容不能为空！", model); }
            if (this._dsrRepository.GetAll().Where(d => d.dsr_DormId == model.dsr_DormId && d.dsr_DormSetId == model.dsr_DormSetId && d.IsDeleted == false).Count() > 0)
            {
                return new OperationResult(OperationResultType.Error, "不能添加相同的记录！", model);
            }
           DormSetRelation dsRelation = new DormSetRelation
            {
                Id = model.Id,
                dsr_DormId = model.dsr_DormId,
                dsr_DormSetId = model.dsr_DormSetId,
                dsr_Private = model.dsr_Private ?? false,
                dsr_Enable = System.DateTime.Now,
                dsr_State = true
            };
            try
            {
                this._dsrRepository.Add(dsRelation);
                DormSetRelationDto result = this.GetByKey(dsRelation.Id);
                return new OperationResult(OperationResultType.Success, "新增成功！", result);
            }
            catch (Exception e)
            {
                return new OperationResult(OperationResultType.Error, "新增失败！", e);
            };
        }

        public OperationResult Delete(DormSetRelationDto model, List<DormitoryDto> dormlist)
        {
            if (model == null) { return new OperationResult(OperationResultType.Error, "不能删除空值！"); }
            //取得dormlist所有setRelation
            var ids = dormlist.Select(l => l.id).ToList();
            List<DormSetRelation> _list = this._dsrRepository.GetAll().Where(d => ids.Contains(d.dsr_DormId)&&d.dsr_Private==false && d.IsDeleted == false).ToList();
            try
            {
                _dsrRepository.UpDeleteList(_list);
                return new OperationResult(OperationResultType.Success, "删除成功！");
            }
            catch (Exception e)
            {
                return new OperationResult(OperationResultType.Error, "删除保存失败！", e);
            }
        }

        public DormSetRelationDto GetByKey(long key)
        {
            var dsRelation = _dsrRepository.GetAll().Where(d => d.Id == key).Select(d => new DormSetRelationDto
            {
                Id = d.Id,
                dsr_DormId = d.dsr_DormId,
                SetType = d.DormSetting.DormOptionType.option_Name,
                dsr_DormSetId = d.dsr_DormSetId,
                SetContent = d.DormSetting.set_Content,
                dsr_Private = (bool)d.dsr_Private,
                dsr_Enable = d.dsr_Enable,
                dsr_State = d.dsr_State,
                dsr_SetTypeId = d.DormSetting.set_TypeId,
                dsr_unEnable = d.dsr_unEnable
            }).SingleOrDefault();
            return dsRelation;
        }

        public IQueryable<DormSetRelationDto> GetSetRelations()
        {
            return _dsrRepository.GetAll().Where(d => d.IsDeleted == false).Select(d => new DormSetRelationDto
            {
                Id = d.Id,
                dsr_DormId = d.dsr_DormId,
                SetType = d.DormSetting.DormOptionType.option_Name,
                dsr_DormSetId = d.dsr_DormSetId,
                SetContent = d.DormSetting.set_Content,
                dsr_Private = (bool)d.dsr_Private,
                dsr_Enable = d.dsr_Enable,
                dsr_State = d.dsr_State,
                dsr_SetTypeId = d.DormSetting.set_TypeId,
                dsr_unEnable = d.dsr_unEnable
            });
        }

        public easyuiGridDto<DormSetRelationDto> GetSetRelations(long dormId, string page, string pagerows)
        {
            int _page = int.Parse(page);
            int _rows = Convert.ToInt16(pagerows);
            IQueryable<DormSetRelationDto> list = GetSetRelations().Where(d => d.dsr_DormId == dormId).OrderBy(t => t.Id);
            int? total = list.Count();
            var data = list.Skip((_page - 1) * _rows).Take(_rows).ToList();
            return new easyuiGridDto<DormSetRelationDto> { total = total, rows = data };
        }

        public OperationResult Update(DormSetRelationDto model)
        {
            if (model == null) { return new OperationResult(OperationResultType.Error, "不能修改空值！"); }
            DormSetRelation dsRelation = _dsrRepository.GetByKey(model.Id);
            dsRelation.dsr_DormId = model.dsr_DormId;
            dsRelation.dsr_DormSetId = model.dsr_DormSetId;
            dsRelation.dsr_Private = model.dsr_Private??false;
            //dsRelation.dsr_Enable = System.DateTime.Now,
            //dsRelation.dsr_State = model.dsr_State;
            dsRelation.dsr_unEnable = model.dsr_unEnable;
            try
            {
                _dsrRepository.Update(dsRelation);
                DormSetRelationDto result = this.GetByKey(model.Id);
                return new OperationResult(OperationResultType.Success, "修改成功！", result);
            }
            catch (Exception e)
            {
                return new OperationResult(OperationResultType.Error, "修改保存失败！", e);
            }
        }

        public OperationResult AddDormlistSetRelation(DormSetRelationDto model, List<DormitoryDto> dormlist)
        {
            if (model.dsr_Cover == true)
            {
                //取得dormlist所有setRelation
                var ids = dormlist.Select(l => l.id).ToList();
                var _list = this._dsrRepository.GetAll().Where(d => ids.Contains(d.dsr_DormId) && d.IsDeleted == false).ToList();
                List<DormSetRelation> dsrList = new List<DormSetRelation>();
                foreach (DormitoryDto item in dormlist)
                {
                    //如果list<setRelation>不存在，则新增一条
                    if (_list.Where(l => l.dsr_DormId == item.id && l.dsr_DormSetId == model.dsr_DormSetId).Count() == 0)
                    {
                        dsrList.Add(new DormSetRelation
                        {
                            Id = model.Id,
                            dsr_DormId = item.id,
                            dsr_DormSetId = model.dsr_DormSetId,
                            dsr_Private = model.dsr_Private ?? false,
                            dsr_Enable = System.DateTime.Now,
                            dsr_State = true
                        });
                    }
                }
                if (dsrList != null)
                {
                    try
                    {
                        this._dsrRepository.AddListDate(dsrList);
                        DormSetRelationDto result = this.GetByKey(dsrList.FirstOrDefault().Id);
                        return new OperationResult(OperationResultType.Success, "批量添加成功！", result);
                    }
                    catch (Exception e)
                    {
                        return new OperationResult(OperationResultType.Error, "批量添加保存失败！", e);
                    }
                }
                return new OperationResult(OperationResultType.Error, "不能保存为空的记录");
            }
            return new OperationResult(OperationResultType.Error, "未设定为覆盖子节点！");
        }
    }
}
