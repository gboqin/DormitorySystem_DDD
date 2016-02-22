using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface IDormSettingService
    {
        OperationResult Add(DormSettingDto model);
        OperationResult Edit(DormSettingDto model);
        OperationResult Delete(DormSettingDto model);
        IQueryable<DormSettingDto> GetAll();
        easyuiGridDto<DormSettingDto> GetList(string page,string rows,string content,string typeid);
        DormSettingDto GetByKey(long key);
    }
}
