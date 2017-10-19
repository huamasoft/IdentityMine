using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Data.Identity.Mine.Entity;

namespace WebApplication1.Identity.Mine.Infrastructure
{
    public static class LocationClaimsProvider
    {

        public static IEnumerable<Claim> GetClaims(IList<ClaimInfo> list)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var claimInfo in list)
            {
                claims.Add(CreateClaim(claimInfo.ClaimType, claimInfo.Value, claimInfo.Issuer));
            }
            return claims;
        }

        private static Claim CreateClaim(string type, string value, string issuer)
        {
            return new Claim(type, value, ClaimValueTypes.String, issuer);
        }
    }
}