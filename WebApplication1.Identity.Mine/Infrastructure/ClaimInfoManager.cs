using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Data.Identity.Mine;
using Data.Identity.Mine.Entity;
using WebApplication1.Identity.Mine.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Identity.Mine.Infrastructure
{
    public class ClaimInfoManager
    {
        public ClaimInfoManager()
        {
            Context = new ApplicationDbContext();
        }
        private ApplicationDbContext Context { get; set; }

        public IQueryable<ClaimInfo> Entitys()
        {
            return Context.ClaimInfos;
        }

        public List<ClaimInfo> GetClaimInfos()
        {
            return Context.ClaimInfos.ToList();
        }

        public List<ClaimInfo> GetClaimInfos(List<string> roleIdList)
        {
            if (roleIdList == null || roleIdList.Count == 0)
            {
                return new List<ClaimInfo>();
            }

            var list = Context.AppRoles.Where(p => roleIdList.Contains(p.Id)).Select(p => p.ClaimInfos).ToList();
            var list1 = new List<ClaimInfo>();
            foreach (var claimInfos in list)
            {
                list1.AddRange(claimInfos);
            }

            return list1.Distinct().ToList();
        }

        public List<ClaimInfo> GetClaimInfo(string issuer, string claimType, string value)
        {
            return
                Context.ClaimInfos.Where(
                    p => p.Issuer.Equals(issuer) && p.ClaimType.Equals(claimType) && p.Value.Equals(value)).ToList();
        }

        public ClaimInfo GetClaimInfoById(int id)
        {
            return Context.ClaimInfos.Find(id);
        }

        public void Create(CreateClaimInfoModel model)
        {
            var claimInfo = new ClaimInfo() { Issuer = model.Issuer, ClaimType = model.ClaimType, Value = model.Value, MethodTypeValue = model.MethodTypeValue };

            Context.ClaimInfos.Add(claimInfo);
            Context.SaveChanges();
        }

        public void Create(List<CreateClaimInfoModel> models)
        {
            var list = models.Select(model => new ClaimInfo() { Issuer = model.Issuer, ClaimType = model.ClaimType, Value = model.Value, MethodTypeValue = model.MethodTypeValue }).ToList();

            Context.ClaimInfos.AddRange(list);
            Context.SaveChanges();
        }

        public void Edit(ClaimInfo claimInfo)
        {
            Context.ClaimInfos.AddOrUpdate(claimInfo);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Context.ClaimInfos.Find(id);
            if (entity != null) Context.ClaimInfos.Remove(entity);
            Context.SaveChanges();
        }

        //public IdentityResult AddRoleToClaimInfo(string roleId, int[] claims)
        //{
        //    AppRole role = (AppRole)Context.Roles.Find(roleId);
        //    var list = Context.ClaimInfos.Where(p => claims.Any(y => y == p.Id)).ToList();
        //    role.ClaimInfos = list;
        //    Context.SaveChanges();
        //    return new IdentityResult();
        //}

        //public IdentityResult RemoveRoleToClaimInfo(string roleId, int[] claims)
        //{
        //    AppRole role = (AppRole)Context.Roles.Find(roleId);
        //    var list = Context.ClaimInfos.Where(p => claims.Any(y => y == p.Id)).ToList();
        //    role.ClaimInfos = role.ClaimInfos
        //    Context.SaveChanges();
        //    return new IdentityResult();
        //}

        public IdentityResult AddRoleToClaimInfo(int claimId, string[] roles)
        {
            if (roles != null)
            {
                var claim = Context.ClaimInfos.Include(p => p.AppRoles).FirstOrDefault(p => p.Id == claimId);
                var list = Context.AppRoles.Where(p => roles.Any(y => y == p.Id)).ToList();
                if (claim != null) claim.AppRoles.AddRange(list);
                Context.SaveChanges();
            }

            return new IdentityResult();
        }

        public IdentityResult RemoveRoleToClaimInfo(int claimId, string[] roles)
        {
            if (roles != null)
            {
                var claim = Context.ClaimInfos.Include(p => p.AppRoles).FirstOrDefault(p => p.Id == claimId);
                var list = Context.AppRoles.Where(p => roles.Any(y => y == p.Id)).ToList();
                if (claim != null) claim.AppRoles = claim.AppRoles.Except(list).ToList();
                Context.SaveChanges();
            }
            return new IdentityResult();
        }
    }
}