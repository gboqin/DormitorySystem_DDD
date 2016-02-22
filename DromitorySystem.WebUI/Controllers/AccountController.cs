using DormitorySystem.Application;
using DormitorySystem.Application.viewModel;
using DormitorySystem.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DromitorySystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "code,password")] LoginDto model)
        {
            MessageDto<UserDto> _message = new MessageDto<UserDto>();
            Message message = new Message();
            message.success = false;
            if (model != null)
            {
                _message = _accountService.Login(model.code, model.password);
                if (_message.success)
                {
                    string UserData = SerialzeHelper.JsonSerialize<UserDto>(_message.model);

                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1,_message.model.usr_Name,DateTime.Now,DateTime.Now.AddHours(4),false,UserData);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));
                    Response.Cookies.Add(cookie);

                    message.success = true;
                    message.message = _message.message;

                    return Json(message);
                }
                else
                {
                    message.message = _message.message;
                    return Json(message);
                }
            }
            message.message = "参数错误，请重新填写！";
            return Json(message);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult LogError()
        {
            return View();
        }
    }
}