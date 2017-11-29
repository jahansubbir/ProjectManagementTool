namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "AssignedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "AssignedBy");
        }
    }
}
