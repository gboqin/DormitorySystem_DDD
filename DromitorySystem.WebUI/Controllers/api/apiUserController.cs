using DormitorySystem.Application;
using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DromitorySystem.WebUI.Controllers.api
{
    public class apiUserController : ApiController
    {
        private IUserService _UserService;
        private ILevelService _LevelService;

        public apiUserController(IUserService userService, ILevelService levelService)
        {
            this._UserService = userService;
            this._LevelService = levelService;
        }

        public IEnumerable<LevelDto> GetLevels()
        {
            return _LevelService.GetAll();
        }

        public easyuiGridDto<UserDto> GetUserGrid(string page = null, string rows = null)
        {          
            return _UserService.GetUsers(page, rows);
        }

        [ResponseType(typeof(UserDto))]
        public IHttpActionResult PostUser(UserDto user)
        {
            OperationResult result = null;
            UserDto _user = null;
            result = _UserService.Add(user);
            if (result.ResultType == OperationResultType.Success)
            {
                _user = result.AppendData as UserDto;
                return CreatedAtRoute("DefaultApi", new { id = _user.Id }, _user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(UserDto))]
        public IHttpActionResult PuteUser(UserDto user)
        {
            try
            {
                _UserService.Update(user);
                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
            catch (Exception e)
            { return BadRequest(ModelState); }
        }

        [ResponseType(typeof(UserDto))]
        public IHttpActionResult PuteDeleteUser(UserDto user)
        {
            try {
                _UserService.UpDeletedate(user);
                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
            catch(Exception e)
            {
                return BadRequest(ModelState);
            }
        }
    }
}
