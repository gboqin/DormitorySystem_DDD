using DormitorySystem.Repositories.EntityFramework;
using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DromitorySystem.WebUI.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            DromityDbContext db = new DromityDbContext();
            List<Level> list = db.Levels.ToList<Level>();
            return View(list);
        }
    }
}