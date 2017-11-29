namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "Name");
        }
    }
}
