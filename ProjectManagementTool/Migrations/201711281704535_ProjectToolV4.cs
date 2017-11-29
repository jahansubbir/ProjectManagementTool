namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectToolV4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Users", new[] { "Project_Id" });
            DropColumn("dbo.Users", "Project_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Project_Id", c => c.Int());
            CreateIndex("dbo.Users", "Project_Id");
            AddForeignKey("dbo.Users", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
