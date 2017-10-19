namespace Data.Identity.Mine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleClaimRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppRoleClaimInfoes",
                c => new
                    {
                        AppRole_Id = c.String(nullable: false, maxLength: 128),
                        ClaimInfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppRole_Id, t.ClaimInfo_Id })
                .ForeignKey("dbo.AspNetRoles", t => t.AppRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.ClaimInfoes", t => t.ClaimInfo_Id, cascadeDelete: true)
                .Index(t => t.AppRole_Id)
                .Index(t => t.ClaimInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppRoleClaimInfoes", "ClaimInfo_Id", "dbo.ClaimInfoes");
            DropForeignKey("dbo.AppRoleClaimInfoes", "AppRole_Id", "dbo.AspNetRoles");
            DropIndex("dbo.AppRoleClaimInfoes", new[] { "ClaimInfo_Id" });
            DropIndex("dbo.AppRoleClaimInfoes", new[] { "AppRole_Id" });
            DropTable("dbo.AppRoleClaimInfoes");
        }
    }
}
