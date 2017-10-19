using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Data.Identity.Mine
{
    /// <summary>
    /// 初始化identity 数据库，但由于ef Migrations Seed 存在，所以弃用
    /// </summary>
    class IdentityDbInit : IDatabaseInitializer<ApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
