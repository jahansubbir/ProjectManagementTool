namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectToolV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 40, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignResources", "UserId", "dbo.Users");
            DropForeignKey("dbo.AssignResources", "ProjectId", "dbo.Projects");
            DropIndex("dbo.AssignResources", new[] { "UserId" });
            DropIndex("dbo.AssignResources", new[] { "ProjectId" });
            DropTable("dbo.AssignResources");
        }
    }
}
