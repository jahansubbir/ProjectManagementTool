using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class Designation
    {
        public int Id { get; set; }
        [DisplayName("Designation")]
        [Required(ErrorMessage = "Designation cannot be empty")]
        public string Name { get; set; }

    }
}