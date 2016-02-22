using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application
{
    public interface IUserService
    {
        OperationResult Add(UserDto model);
        void UpDeletedate(UserDto model);
        void Update(UserDto model);
        void Delete(UserDto model);
        IQueryable<UserDto> GetUsers();
        easyuiGridDto<UserDto> GetUsers(string page, string pagerows);
        User GetByKey(long key);
    }
}
