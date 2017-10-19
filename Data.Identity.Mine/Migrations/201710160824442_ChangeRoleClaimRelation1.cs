namespace Data.Identity.Mine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleClaimRelation1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClaimInfoes", "AppRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRoles", "ClaimInfo_Id", "dbo.ClaimInfoes");
            DropIndex("dbo.ClaimInfoes", new[] { "AppRole_Id" });
            DropIndex("dbo.AspNetRoles", new[] { "ClaimInfo_Id" });
            CreateTable(
                "dbo.ClaimInfoAppRoles",
                c => new
                    {
                        ClaimInfo_Id = c.Int(nullable: false),
                        AppRole_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ClaimInfo_Id, t.AppRole_Id })
                .ForeignKey("dbo.ClaimInfoes", t => t.ClaimInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.AppRole_Id, cascadeDelete: true)
                .Index(t => t.ClaimInfo_Id)
                .Index(t => t.AppRole_Id);
            
            DropColumn("dbo.ClaimInfoes", "AppRole_Id");
            DropColumn("dbo.AspNetRoles", "ClaimInfo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetRoles", "ClaimInfo_Id", c => c.Int());
            AddColumn("dbo.ClaimInfoes", "AppRole_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ClaimInfoAppRoles", "AppRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.ClaimInfoAppRoles", "ClaimInfo_Id", "dbo.ClaimInfoes");
            DropIndex("dbo.ClaimInfoAppRoles", new[] { "AppRole_Id" });
            DropIndex("dbo.ClaimInfoAppRoles", new[] { "ClaimInfo_Id" });
            DropTable("dbo.ClaimInfoAppRoles");
            CreateIndex("dbo.AspNetRoles", "ClaimInfo_Id");
            CreateIndex("dbo.ClaimInfoes", "AppRole_Id");
            AddForeignKey("dbo.AspNetRoles", "ClaimInfo_Id", "dbo.ClaimInfoes", "Id");
            AddForeignKey("dbo.ClaimInfoes", "AppRole_Id", "dbo.AspNetRoles", "Id");
        }
    }
}
