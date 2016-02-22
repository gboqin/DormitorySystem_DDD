using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DromitorySystem.WebUI.Controllers
{
    public class DormitoryController : Controller
    {
        // GET: Dormitory
        public ActionResult Index()
        {
            return View();
        }
    }
}