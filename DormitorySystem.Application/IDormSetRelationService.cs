using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface IDormSetRelationService
    {
        OperationResult Add(DormSetRelationDto model);
        OperationResult AddDormlistSetRelation(DormSetRelationDto model, List<DormitoryDto> dormlist);
        OperationResult Update(DormSetRelationDto model);
        OperationResult Delete(DormSetRelationDto model, List<DormitoryDto> dormlist);
        IQueryable<DormSetRelationDto> GetSetRelations();
        easyuiGridDto<DormSetRelationDto> GetSetRelations(long dormId,string page, string pagerows);
        DormSetRelationDto GetByKey(long key);
    }
}
