using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface IDormitoryService
    {
        OperationResult Add(BuildDto model);
        OperationResult Delete(DormitoryDto model);
        List<DormitoryDto> GetRootData(string Id);
        List<DormitoryDto> GetDormitories(long Id);
    }
}
