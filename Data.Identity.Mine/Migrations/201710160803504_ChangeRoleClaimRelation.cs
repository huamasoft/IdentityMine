namespace Data.Identity.Mine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleClaimRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppRoleClaimInfoes", "AppRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.AppRoleClaimInfoes", "ClaimInfo_Id", "dbo.ClaimInfoes");
            DropIndex("dbo.AppRoleClaimInfoes", new[] { "AppRole_Id" });
            DropIndex("dbo.AppRoleClaimInfoes", new[] { "ClaimInfo_Id" });
            AddColumn("dbo.ClaimInfoes", "AppRole_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetRoles", "ClaimInfo_Id", c => c.Int());
            CreateIndex("dbo.ClaimInfoes", "AppRole_Id");
            CreateIndex("dbo.AspNetRoles", "ClaimInfo_Id");
            AddForeignKey("dbo.ClaimInfoes", "AppRole_Id", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetRoles", "ClaimInfo_Id", "dbo.ClaimInfoes", "Id");
            DropTable("dbo.AppRoleClaimInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppRoleClaimInfoes",
                c => new
                    {
                        AppRole_Id = c.String(nullable: false, maxLength: 128),
                        ClaimInfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppRole_Id, t.ClaimInfo_Id });
            
            DropForeignKey("dbo.AspNetRoles", "ClaimInfo_Id", "dbo.ClaimInfoes");
            DropForeignKey("dbo.ClaimInfoes", "AppRole_Id", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetRoles", new[] { "ClaimInfo_Id" });
            DropIndex("dbo.ClaimInfoes", new[] { "AppRole_Id" });
            DropColumn("dbo.AspNetRoles", "ClaimInfo_Id");
            DropColumn("dbo.ClaimInfoes", "AppRole_Id");
            CreateIndex("dbo.AppRoleClaimInfoes", "ClaimInfo_Id");
            CreateIndex("dbo.AppRoleClaimInfoes", "AppRole_Id");
            AddForeignKey("dbo.AppRoleClaimInfoes", "ClaimInfo_Id", "dbo.ClaimInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppRoleClaimInfoes", "AppRole_Id", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
