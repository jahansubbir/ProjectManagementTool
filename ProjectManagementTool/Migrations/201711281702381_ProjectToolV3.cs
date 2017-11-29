namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectToolV3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Project_Id", c => c.Int());
            CreateIndex("dbo.Users", "Project_Id");
            AddForeignKey("dbo.Users", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Users", new[] { "Project_Id" });
            DropColumn("dbo.Users", "Project_Id");
        }
    }
}
