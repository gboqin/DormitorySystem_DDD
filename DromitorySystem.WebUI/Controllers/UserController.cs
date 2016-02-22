using DormitorySystem.Application;
using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DromitorySystem.WebUI.Controllers
{
    public class UserController : Controller
    {
        //生成测试数据用
        private IUserService _UserService;
        private ILevelService _LevelService;
        private IDormOptiontypeService _DOService;
        private IDormSettingService _DSService;

        public UserController(IUserService userService, ILevelService levelService, IDormOptiontypeService DOService, IDormSettingService DSSerivce)
        {
            this._UserService = userService;
            this._LevelService = levelService;
            this._DOService = DOService;
            this._DSService = DSSerivce;
        }

        // GET: Product
        public ActionResult Index()
        {
            var list = _UserService.GetUsers().ToList();
            return View();
        }
    }
}