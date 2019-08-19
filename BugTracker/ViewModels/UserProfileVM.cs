using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class UserProfileVM
    {
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "First Name Cannot Be Greater Than 50 Characters")]
        [MinLength(1, ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last Name Cannot Be Greater Than 50 Characters")]
        [MinLength(1, ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(20, ErrorMessage = "Display Name Cannot Be Greater Than 20 Characters")]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
       
     

    } 
}