using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISBN.Web.Areas.Admin.Models
{
    public class Role
    {
        [Required]
        [Display(Name="Role Name")]
        [StringLength(30, ErrorMessage="A role name cannot exceed 30 characters.")]
        public string RoleName { get; set; }
    }
}