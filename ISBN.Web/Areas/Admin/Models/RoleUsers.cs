using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISBN.Web.Areas.Admin.Models
{
    public class RoleUsers
    {
        [Display(Name="Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string[] UserNames { get; set; }
    }
}