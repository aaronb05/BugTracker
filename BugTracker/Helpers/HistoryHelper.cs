using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BugTracker.Helpers
{
    public class HistoryHelper
    {
      
        public void RecordHistory(Ticket oldTicket, Ticket newTicket)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            foreach (var property in WebConfigurationManager.AppSettings["TrackedHistoryProperties"].Split(','))
            {
                var oldValue = oldTicket.GetType().GetProperty(property).GetValue(oldTicket, null).ToString();
                var newValue = newTicket.GetType().GetProperty(property).GetValue(newTicket, null).ToString();

                if (oldValue != newValue)
                {
                    var newHistory = new TicketHistory
                    {
                        UserId = HttpContext.Current.User.Identity.GetUserId(),
                        Updated = (DateTime)newTicket.Updated,
                        PropertyName = property,
                        OldValue = Utilities.MakeReadable(property, oldValue),
                        NewValue = Utilities.MakeReadable(property, newValue),
                        TicketId = newTicket.Id

                    };

                    db.TicketHistories.Add(newHistory);
                }

            }
            db.SaveChanges();
        }

    }
}