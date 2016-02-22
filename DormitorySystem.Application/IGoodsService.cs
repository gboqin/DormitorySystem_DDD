using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface IGoodsService
    {
        OperationResult Add(GoodsDto model);
        OperationResult Edit(GoodsDto model);
        OperationResult Delete(GoodsDto model);
        IQueryable<GoodsDto> GetAll();
        easyuiGridDto<GoodsDto> GetAll(string page,string pagerows,string name);
        GoodsDto GetByKey(long key);
    }
}
