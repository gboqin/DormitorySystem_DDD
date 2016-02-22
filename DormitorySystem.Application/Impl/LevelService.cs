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
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        public LevelService(ILevelRepository levelRepository)
        {
            this._levelRepository = levelRepository;
        }
        public IEnumerable<LevelDto> GetAll()
        {
            return _levelRepository.GetAll().Where(l=>l.IsDeleted==false).Select(l => new LevelDto { Id = l.Id, lev_Text = l.lev_Text }).ToList();
        }
    }
}
