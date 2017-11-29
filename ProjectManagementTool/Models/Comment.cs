using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        [DisplayName("Project")]
        public int ProjectId { get; set; }
        [DisplayName("Task")]
        public int TaskId { get; set; }
        public string Description { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
       // [ForeignKey("TaskId")]
        //public ProjectTask ProjectTask { get; set; }

    }
}