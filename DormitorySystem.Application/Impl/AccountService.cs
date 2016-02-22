using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitorySystem.Application.viewModel;
using DromitorySystem.Domain.Repositories;

namespace DormitorySystem.Application.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository; 
        public AccountService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public MessageDto<UserDto> Login(string code, string password)
        {
            MessageDto<UserDto> _message = new MessageDto<UserDto>();
            UserDto userDto = _userRepository.GetAll().Where(u => u.usr_Code == code && u.usr_Password == password)
                              .Select(u => new UserDto { Id = u.Id, usr_Code = u.usr_Code, usr_Name = u.usr_Name, usr_Password = u.usr_Password, usr_lev_Id = u.usr_lev_Id, usr_Level = u.Level.lev_Text })
                              .SingleOrDefault();
            if (userDto == null) {
                _message.success = false;
                _message.message = "登录失败，工号或密码不正确！";
            }
            else
            {
                _message.success = true;
                _message.message = "登录成功！";
            }
            _message.model = userDto;
            return _message;
        }
    }
}
