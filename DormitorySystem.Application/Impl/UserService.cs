using AutoMapper;
using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Entities;
using DromitorySystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.Impl
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public IQueryable<UserDto> GetUsers()
        {
            return _userRepository.GetAll().Where(u=>u.IsDeleted==false).Select(u => new UserDto { Id = u.Id, usr_Name = u.usr_Name, usr_Code = u.usr_Code, usr_Password = u.usr_Password,usr_lev_Id=u.usr_lev_Id, usr_Level = u.Level.lev_Text });
        }

        public OperationResult Add(UserDto model)
        {
            
            User user = new User {usr_Code=model.usr_Code,usr_Name=model.usr_Name,usr_Password=model.usr_Password,usr_lev_Id=model.usr_lev_Id };
            _userRepository.Add(user);
            UserDto _user = new UserDto{Id=user.Id,usr_Code = user.usr_Code,usr_Name= user.usr_Name,usr_Password = user.usr_Password,usr_lev_Id = user.usr_lev_Id,usr_Level=model.usr_Level};
            return new OperationResult(OperationResultType.Success, "添加成功！", _user);
        }

        public User GetByKey(long key)
        {
            return _userRepository.GetByKey(key);
        }

        public void Update(UserDto model)
        {
            User user = GetByKey(model.Id);
            user.usr_Code = model.usr_Code;
            user.usr_Name = model.usr_Name;
            user.usr_Password = model.usr_Password;
            user.usr_lev_Id = model.usr_lev_Id;
            _userRepository.Update(user);
        }

        public void UpDeletedate(UserDto model)
        {
            User user = GetByKey(model.Id);
            user.usr_Code = model.usr_Code;
            user.usr_Name = model.usr_Name;
            user.usr_Password = model.usr_Password;
            user.usr_lev_Id = model.usr_lev_Id;
            user.IsDeleted = true;
            _userRepository.Update(user);
        }

        public void Delete(UserDto model)
        {
            User user = GetByKey(model.Id);
            _userRepository.Remove(user);
        }

        public easyuiGridDto<UserDto> GetUsers(string page, string pagerows)
        {
            int _page = string.IsNullOrEmpty(page) ? 1: int.Parse(page);
            int _rows = string.IsNullOrEmpty(pagerows)?15:int.Parse(pagerows);
            IQueryable<UserDto> list = GetUsers().OrderBy(u=>u.Id);
            int? total = list.Count();
            var data = list.Skip((_page - 1) * _rows).Take(_rows).ToList();
            easyuiGridDto<UserDto> result = new easyuiGridDto<UserDto>();
            result.total = total;
            result.rows = data;
            return result;
        }
    }
}
