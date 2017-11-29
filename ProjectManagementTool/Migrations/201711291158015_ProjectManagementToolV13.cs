namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV13 : DbMigration
    {
        public override void Up()
        {
          //  AddColumn("dbo.ViewProjectDetails", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.ViewProjectDetails", "UserId");
        }
    }
}
