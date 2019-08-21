using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Display(Name = "Project Name")]
        public int ProjectId { get; set; }
        [StringLength( 50, ErrorMessage = "Title must be between {2} and {1} characters long", MinimumLength = 3)]
        public string Title { get; set; }
        [StringLength(50, ErrorMessage = "Descriptions must be between {2} and {1} characters long", MinimumLength = 5)]
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        [Display(Name = "Ticket Type")]
        public int TicketTypeId { get; set; }
        [Display(Name = "Ticket Priority")]
        public int TicketPriorityId { get; set; }
        [Display(Name ="Ticket Status")]
        public int TicketStatusId { get; set; }
        [Display(Name = "Submitter")]
        public string OwnerUserId { get; set; }
        [Display(Name = "Developer")]
        public string AssignedToUserId { get; set; }


        [Display(Name = "Project Name")]
        public virtual Project Project { get; set; }
        [Display(Name = "Ticket Type")]
        public virtual TicketType TicketType { get; set; }
        [Display(Name = "Ticket Priority")]
        public virtual TicketPriority TicketPriority { get; set; }
        [Display(Name = "Ticket Status")]
        public virtual TicketStatus TicketStatus { get; set; }
        [Display(Name = "Submitter")]
        public virtual ApplicationUser OwnerUser { get; set; }
        [Display(Name = "Developer")]
        public virtual ApplicationUser AssignedToUser { get; set; } //is this user or userId?

        public virtual ICollection<TicketComment> TicketComments {get; set;}
        public virtual ICollection<TicketAttachment> TicketAttachment { get; set; }
        public virtual ICollection<TicketHistory> TicketHistory { get; set; }
        public virtual ICollection<TicketNotification> TicketNotification { get; set; }

        public Ticket()
        {
            TicketComments = new HashSet<TicketComment>();
            TicketAttachment = new HashSet<TicketAttachment>();
            TicketHistory = new HashSet<TicketHistory>();
            TicketNotification = new HashSet<TicketNotification>();

        }


    }
}