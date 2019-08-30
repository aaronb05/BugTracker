using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace BugTracker.Helpers
{
    public class NotificationHelper
    {

        private static ApplicationDbContext db = new ApplicationDbContext();
        private static ProjectManagerHelper projectHelper = new ProjectManagerHelper();
        private static UserRolesHelper roleHelper = new UserRolesHelper();

        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {
            //CreateAssignmentNotification(oldTicket, newTicket);
            CreateChangeNotification(oldTicket, newTicket);

        }

        public static void CreateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var noChange = (oldTicket.AssignedToUserId == newTicket.AssignedToUserId);
            var assignment = (string.IsNullOrEmpty(oldTicket.AssignedToUserId));
            var unassignment = (string.IsNullOrEmpty(newTicket.AssignedToUserId));

            if (noChange)
                return;

            if (assignment)
                GenerateAssignmentNotification(oldTicket, newTicket);
            else if (unassignment)
                GenerateUnAssignmentNotification(oldTicket, newTicket);
            else
            {
                GenerateAssignmentNotification(oldTicket, newTicket);
                GenerateUnAssignmentNotification(oldTicket, newTicket);
            }
        }

        private static void GenerateUnAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {


            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were unassigned from ticket Id {newTicket.Title} on {DateTime.Now}",
                Read = false,
                RecipientId = oldTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"Please acknowledge that you have read this notification by marking it as READ",
                TicketId = newTicket.Id
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();

        }

        private static void GenerateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were assigned to ticket Id {newTicket.Id} on {DateTime.Now}",
                Read = false,
                RecipientId = newTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"Please acknowledge that you have read this notification",
                TicketId = newTicket.Id
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();

        }

        private void CreateChangeNotification(Ticket oldTicket, Ticket newTicket)
        {
            var messageBody = new StringBuilder();

            foreach (var property in WebConfigurationManager.AppSettings["TrackedTicketProperties"].Split(','))
            {
                var oldValue = Utilities.MakeReadable(property, oldTicket.GetType().GetProperty(property).GetValue(oldTicket, null).ToString());
                var newValue = Utilities.MakeReadable(property, newTicket.GetType().GetProperty(property).GetValue(newTicket, null).ToString());

                if (oldValue != newValue)
                {
                    //messageBody.AppendLine(new String('-', 45));
                    messageBody.AppendLine($"A change was made to Property: {property}.");
                    messageBody.AppendLine($"The old value was: {oldValue.ToString()}");
                    messageBody.AppendLine($"The new value is: {newValue.ToString()}");
                }
            }

            if (!string.IsNullOrEmpty(messageBody.ToString()))
            {
                var message = new StringBuilder();
                message.AppendLine($"Changes were made to Ticket Id: {newTicket.Id} on {newTicket.Updated.GetValueOrDefault().ToString("MMM d, yyyy")}");
                message.AppendLine(messageBody.ToString());
                var senderId = HttpContext.Current.User.Identity.GetUserId();

                var notification = new TicketNotification
                {
                    TicketId = newTicket.Id,
                    Created = DateTime.Now,
                    Subject = $"Ticket Id: {newTicket.Id} has changed",
                    RecipientId = oldTicket.AssignedToUserId,
                    SenderId = senderId,
                    NotificationBody = message.ToString(),
                    Read = false
                };

                db.TicketNotifications.Add(notification);
                db.SaveChanges();
            }
        }

        //public static void CreateProjectTicketNotification(Project oldProj, Project newProj)
        //{
        //    var oldProjTickets = oldProj.Tickets.Count();
        //    var newProjTickets = newProj.Tickets.Count();

        //    var noChange = (oldProjTickets <= newProjTickets);
        //    var addittion = (oldProjTickets > newProjTickets);


        //    if (noChange)
        //        return;

        //    if (addittion)
        //    {
        //       GenerateNewTicketNotification(oldProj, newProj);
        //    }
        //}


        //private static void GenerateNewTicketNotification(Project oldProj, Project newProj)
        //{
        //    var projectId = oldProj.Id;
        //    var projectManager = projectHelper.UsersInRoleOnProject(projectId, "Project Manager").FirstOrDefault();
            

        //    var notification = new TicketNotification
        //    {
        //        Created = DateTime.Now,
        //        Subject = $"A new ticket has been added to {newProj.Name} on {DateTime.Now}",
        //        Read = false,
        //        RecipientId = projectManager,
        //        SenderId = HttpContext.Current.User.Identity.GetUserId(),
        //        NotificationBody = $"Please acknowledge that you have read this notification",
        //        TicketId = newProj.Id
        //    };

        //    db.TicketNotifications.Add(notification);
        //    db.SaveChanges();

        //}



        public int GetNewUserNotificationCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Where(t => t.RecipientId == userId && !t.Read).Count();
        }

        public int GetAllUserNotificationCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Where(t => t.RecipientId == userId).Count();
        }

        public List<TicketNotification> GetUnreadUserNotifications()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Where(t => t.RecipientId == userId && !t.Read).ToList();

        }

        public List<TicketNotification> GetAllUserNotifications()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.ToList();

        }
    }
}
