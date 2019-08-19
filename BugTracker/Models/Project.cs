using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Title must be between {2} and {1} characters long", MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Description must be between {2} and {1} characters long", MinimumLength = 3)]
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser>Users { get; set; }
        public Project()
        {
            Tickets = new HashSet<Ticket>();
            Users = new HashSet<ApplicationUser>();
        }
    }
}