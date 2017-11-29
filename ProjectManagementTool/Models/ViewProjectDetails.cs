using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class ViewProjectDetails
    {
        public int Id { get; set; }
        public string Project { get; set; }
        public string CodeName { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string AssignedTo { get; set; }
        public string Task { get; set; }

    }
}