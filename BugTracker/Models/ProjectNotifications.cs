﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ProjectNotifications
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Subject { get; set; }
        public string NotificationBody { get; set; }
        public bool Read { get; set; }

        public virtual Project Project { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
        public virtual ApplicationUser Sender { get; set; }

    }
}