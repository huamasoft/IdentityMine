using Data.Identity.Mine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Identity.Mine.Infrastructure;

namespace WebApplication1.Identity.Mine.Controllers
{
    public class Role1Controller : Controller
    {
        [ClaimsAccess(ClaimType = "business1", Value = "r1", MethodTypeValue = MethodType.Get)]
        // GET: Role1
        public ActionResult Index()
        {
            return View();
        }

        [ClaimsAccess(ClaimType = "business1", Value = "r1", MethodTypeValue = MethodType.Post)]
        public JsonResult Create()
        {
            return Json(0);
        }

        [ClaimsAccess(ClaimType = "business1", Value = "r1", MethodTypeValue = MethodType.Put)]
        public JsonResult Edit()
        {
            return Json(0);
        }

        [ClaimsAccess(ClaimType = "business1", Value = "r1", MethodTypeValue = MethodType.Delete)]
        public JsonResult Delete()
        {
            return Json(0);
        }
    }
}