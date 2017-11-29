using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class User
    {
        [Key]
        [Column(TypeName = "varchar")]
        [MaxLength(40)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Please write your name!")]
        [Column(TypeName = "varchar")]
        [MaxLength(80)]
        public string Name { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(80)]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Give your email address!")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string PasswordHash { get; set; }
        [DisplayName("Status")]
        public bool Active { get; set; }
        [Required(ErrorMessage = "Select Designation")]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

    }
}