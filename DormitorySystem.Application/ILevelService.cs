﻿using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface ILevelService
    {
        IEnumerable<LevelDto> GetAll();
    }
}
