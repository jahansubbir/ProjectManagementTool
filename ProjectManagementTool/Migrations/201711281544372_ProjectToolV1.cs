namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectToolV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150, unicode: false),
                        CodeName = c.String(maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 250, unicode: false),
                        PossibleStartDate = c.DateTime(nullable: false),
                        PossibleEndDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        UploadedFileName = c.String(maxLength: 40, unicode: false),
                        ProjectStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectStatus", t => t.ProjectStatusId, cascadeDelete: true)
                .Index(t => t.ProjectStatusId);
            
            CreateTable(
                "dbo.ProjectStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 40, unicode: false),
                        Name = c.String(nullable: false, maxLength: 80, unicode: false),
                        Email = c.String(nullable: false, maxLength: 80, unicode: false),
                        PasswordHash = c.String(),
                        Active = c.Boolean(nullable: false),
                        DesignationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.DesignationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Projects", "ProjectStatusId", "dbo.ProjectStatus");
            DropIndex("dbo.Users", new[] { "DesignationId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Projects", new[] { "ProjectStatusId" });
            DropTable("dbo.Users");
            DropTable("dbo.ProjectStatus");
            DropTable("dbo.Projects");
            DropTable("dbo.Designations");
        }
    }
}
