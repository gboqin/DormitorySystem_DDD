﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public class TreeDto
    {
        public long id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public List<TreeDto> children { get; set; }
    }
}
