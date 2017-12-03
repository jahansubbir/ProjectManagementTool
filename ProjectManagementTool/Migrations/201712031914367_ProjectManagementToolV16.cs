namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV16 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProjectTasks", name: "UserId", newName: "AssignTo");
            RenameIndex(table: "dbo.ProjectTasks", name: "IX_UserId", newName: "IX_AssignTo");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProjectTasks", name: "IX_AssignTo", newName: "IX_UserId");
            RenameColumn(table: "dbo.ProjectTasks", name: "AssignTo", newName: "UserId");
        }
    }
}
