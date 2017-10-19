using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data.Identity.Mine;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication1.Identity.Mine.Infrastructure;
using WebApplication1.Identity.Mine.Models;
using System.Reflection;
using Data.Identity.Mine.Entity;

namespace WebApplication1.Identity.Mine.Controllers
{
    public class ClaimInfoAdminController : Controller
    {
        // GET: ClaimAdmin
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            var abs = Assembly.GetExecutingAssembly();
            var ts = abs.GetTypes();
            var list = new List<CreateClaimInfoModel>();


            foreach (Type type in ts)
            {
                if (type.BaseType != null && type.BaseType == typeof(Controller))
                {
                    var ms = type.GetMethods();
                    foreach (MethodInfo methodInfo in ms)
                    {
                        foreach (var methodAttribute in methodInfo.CustomAttributes)
                        {
                            #region MyRegion
                            //if (methodAttribute.AttributeType == typeof(DescriperAttribute))
                            //{
                            //    var nas = methodAttribute.NamedArguments;
                            //    if (nas != null)
                            //        foreach (CustomAttributeNamedArgument namedArgument in nas)
                            //        {
                            //            Console.WriteLine("CustomAttributeNamedArgument:");
                            //            Console.WriteLine("name:" + namedArgument.MemberName + "||value:" + namedArgument.TypedValue.Value);
                            //        }
                            //}
                            //else 
                            #endregion
                            if (methodAttribute.AttributeType == typeof(ClaimsAccessAttribute))
                            {
                                #region MyRegion
                                var nas = methodAttribute.NamedArguments;
                                if (nas != null)
                                {
                                    var claimIfo = new CreateClaimInfoModel();

                                    foreach (CustomAttributeNamedArgument namedArgument in nas)
                                    {
                                        //Console.WriteLine("MyAttribute:");
                                        //Console.WriteLine("name:" + namedArgument.MemberName + "||value:" + namedArgument.TypedValue.Value);

                                        if (namedArgument.MemberName.Equals("Issuer"))
                                        {
                                            claimIfo.Issuer = (string)namedArgument.TypedValue.Value;
                                        }
                                        else if (namedArgument.MemberName.Equals("ClaimType"))
                                        {
                                            claimIfo.ClaimType = (string)namedArgument.TypedValue.Value;
                                        }
                                        else if (namedArgument.MemberName.Equals("Value"))
                                        {
                                            claimIfo.Value = (string)namedArgument.TypedValue.Value;
                                        }
                                        else if (namedArgument.MemberName.Equals("MethodTypeValue"))
                                        {
                                            claimIfo.MethodTypeValue = (MethodType)namedArgument.TypedValue.Value;
                                        }
                                    }

                                    list.Add(claimIfo);
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
            //todo 存在问题：只能在第一次初始插入claim
            if (ClaimInfoManager.GetClaimInfos().Count == 0)
            {
                ClaimInfoManager.Create(list);
            }

            //return View(list);
            var list1 = ClaimInfoManager.Entitys().Include(p => p.AppRoles).ToList();
            return View(list1);
        }

        /// <summary>
        /// 建立 角色和claim|| 用户和calim  的关系表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var claimInof = ClaimInfoManager.GetClaimInfoById(id);
            //RoleManager.ro
            //string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            //todo bug
            var entity = ClaimInfoManager.Entitys().Include(p => p.AppRoles).FirstOrDefault(p => p.Id == id);

            //IEnumerable<AppRole> members = RoleManager.Roles.Where(p => p.ClaimInfos.Contains(claimInof)).ToList();
            IEnumerable<AppRole> members = entity.AppRoles;
            var roleList = RoleManager.Roles.ToList();
            var nonMembers = roleList.Where(appRole => members.Count(p => p.Id.Equals(appRole.Id)) == 0).ToList();

            return View(new ClaimInfoEditModel
            {
                ClaimInfo = claimInof,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ClaimInfoModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                result = ClaimInfoManager.AddRoleToClaimInfo(model.ClaimId, model.IdsToAdd);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                result = ClaimInfoManager.RemoveRoleToClaimInfo(model.ClaimId, model.IdsToDelete);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Role Not Found" });
        }

        private ClaimInfoManager ClaimInfoManager
        {
            get
            {
                return new ClaimInfoManager();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}