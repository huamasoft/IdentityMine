using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Identity.Mine.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View(new Peopel() { Age = 1 });
        }

        public JsonResult Save(int age)
        {
            return Json(1);
        }
    }

    public class Peopel
    {
        public int Age { get; set; }
    }
}