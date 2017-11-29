namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Priorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 40, unicode: false),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        PriorityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Priorities", t => t.PriorityId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId)
                .Index(t => t.PriorityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProjectTasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectTasks", "PriorityId", "dbo.Priorities");
            DropIndex("dbo.ProjectTasks", new[] { "PriorityId" });
            DropIndex("dbo.ProjectTasks", new[] { "UserId" });
            DropIndex("dbo.ProjectTasks", new[] { "ProjectId" });
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.Priorities");
        }
    }
}
