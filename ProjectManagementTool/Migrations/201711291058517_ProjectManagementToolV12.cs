namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectManagementToolV12 : DbMigration
    {
        public override void Up()
        {
           /* CreateTable(
                "dbo.ViewProjectDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Project = c.String(),
                        CodeName = c.String(),
                        Status = c.String(),
                        AssignedTo = c.String(),
                        Task = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ProjectDetailViewModels");*/
        }
        
        public override void Down()
        {/*
            CreateTable(
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
            
            DropTable("dbo.ViewProjectDetails");*/
        }
    }
}
