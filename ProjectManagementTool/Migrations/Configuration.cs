using ProjectManagementTool.Models;

namespace ProjectManagementTool.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectManagementTool.Context.ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectManagementTool.Context.ProjectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Priorities.AddOrUpdate(
            //    new Priority() {Name="High"},
            //    new Priority() { Name = "Medium" },
            //    new Priority() { Name = "Low" });
            //context.Designations.AddOrUpdate(
            //    new Designation() {Name = "IT Admin"},
            //    new Designation() { Name = "Project Manager" });
            //context.ProjectStatus.AddOrUpdate(
            //    new ProjectStatus() {Status = "Not Started"},
            //    new ProjectStatus() {Status = "Started"},
            //    new ProjectStatus() {Status = "Completed"},
            //    new ProjectStatus() {Status = "Cancelled"});
        }
    }
}
