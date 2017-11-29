using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        [DisplayName("Project")]
        public int ProjectId { get; set; }

        public string AssignedBy { get; set; }
        [DisplayName("Assign To")]
        public string UserId { get; set; }
        [DisplayName("Task")]
        public string Name { get; set; }

        public string Description { get; set; }
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }
        [DisplayName("Priority")]
        public int PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}