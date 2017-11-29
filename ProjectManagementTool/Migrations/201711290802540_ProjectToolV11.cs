namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectToolV11 : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.ProjectDetailViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Project = c.String(),
                        CodeName = c.String(),
                        Status = c.String(),
                        AssignTo = c.String(),
                        UserId = c.String(),
                        Designation = c.String(),
                        Task = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            */
        }
        
        public override void Down()
        {
            //DropTable("dbo.ProjectDetailViewModels");
        }
    }
}
