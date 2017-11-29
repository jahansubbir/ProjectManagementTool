using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(150)]
        public string Name { get; set; }
        [DisplayName("Code Name")]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string CodeName { get; set; }
        [MaxLength(250)]
        [Column(TypeName = "varchar")]
        public string Description { get; set; }
        [DisplayName("Possible Start Date")]
        public DateTime PossibleStartDate { get; set; }
        [DisplayName("Possible End Date")]
        public DateTime PossibleEndDate { get; set; }
        public int Duration { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(40)]
        [DisplayName("Upload File")]
        public string UploadedFileName { get; set; }
        [DisplayName("Status")]
        public int ProjectStatusId { get; set; }
        [ForeignKey("ProjectStatusId")]
        public ProjectStatus ProjectStatus { get; set; }

        //public ICollection<User> Users { get; set; }
    }
}