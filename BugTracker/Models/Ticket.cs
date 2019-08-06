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
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        [Display(Name = "Ticket Type")]
        public int TicketTypeId { get; set; }
        [Display(Name = "Ticket Priority")]
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        public string OwnerUserId { get; set; }
        public string AssignedToUserId { get; set; }


        
        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }

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