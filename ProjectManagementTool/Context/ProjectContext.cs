using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.Context
{
    public class ProjectContext:DbContext
    {
        public ProjectContext():base("ProjectConnection")
        {
            
        }

       // public DbSet<ProjectDetailViewModel> ViewProject { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AssignResource> AssignResources { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ViewProjectDetails> ViewProjectDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().HasRequired(a => a.Project).WithMany().WillCascadeOnDelete();
           // modelBuilder.Entity<Comment>().HasRequired(a=>a.ProjectTask).WithMany().WillCascadeOnDelete();
            
        }
    }
}