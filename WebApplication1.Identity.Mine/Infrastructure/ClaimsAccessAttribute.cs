using Data.Identity.Mine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Identity.Mine.Infrastructure
{
    public class ClaimsAccessAttribute : AuthorizeAttribute
    {
        //public ClaimInfo ClaimInfo { get; set; }
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }
        public MethodType MethodTypeValue { get; set; }

        protected override bool AuthorizeCore(HttpContextBase context)
        {
            //验证 请求方法
            if (MethodTypeValue != MethodType.None)
            {

                string name = Enum.GetName(typeof (MethodType), Convert.ToInt32(MethodTypeValue));

                if (name != null && context.Request.HttpMethod.ToLower() != name.ToLower())
                {
                    return false;
                }
            }

            //验证身份
            return context.User.Identity.IsAuthenticated
                   && context.User.Identity is ClaimsIdentity
                   && ((ClaimsIdentity) context.User.Identity).HasClaim(x =>
                       (x.Issuer == Issuer || x.Issuer == "LOCAL AUTHORITY") && x.Type == ClaimType && x.Value == Value
                       );
        }
    }

}