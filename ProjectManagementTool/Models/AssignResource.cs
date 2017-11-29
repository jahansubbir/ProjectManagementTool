using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class AssignResource
    {
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string UserId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }

    }
}