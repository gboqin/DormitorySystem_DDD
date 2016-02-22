using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface IDormOptiontypeService
    {
        OperationResult Add(DormOptiontypeDto model);
        OperationResult Edit(DormOptiontypeDto model);
        OperationResult Delete(DormOptiontypeDto model);
        IQueryable<DormOptiontypeDto> GetAll();
        easyuiGridDto<DormOptiontypeDto> GetAll(string page, string pagerows, string name);
        DormOptiontypeDto GetByKey(long key);
    }
}
