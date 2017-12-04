namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV17 : DbMigration
    {
        public override void Up()
        {
           /* CreateIndex("dbo.Comments", "TaskId");
            AddForeignKey("dbo.Comments", "TaskId", "dbo.ProjectTasks", "Id", cascadeDelete: true);*/
        }
        
        public override void Down()
        {
           /* DropForeignKey("dbo.Comments", "TaskId", "dbo.ProjectTasks");
            DropIndex("dbo.Comments", new[] { "TaskId" });*/
        }
    }
}
