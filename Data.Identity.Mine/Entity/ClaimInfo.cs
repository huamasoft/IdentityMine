using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity.Mine.Entity
{
    public class ClaimInfo
    {
        public int Id { get; set; }
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }
        public MethodType MethodTypeValue { get; set; }
        public List<AppRole> AppRoles { get; set; }
    }
    public enum MethodType
    {
        None,
        Get,
        Post,
        Put,
        Delete
    }
}
