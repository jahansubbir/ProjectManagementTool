namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "TaskId", "dbo.ProjectTasks");
            DropIndex("dbo.Comments", new[] { "TaskId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Comments", "TaskId");
            AddForeignKey("dbo.Comments", "TaskId", "dbo.ProjectTasks", "Id", cascadeDelete: true);
        }
    }
}
