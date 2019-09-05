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
    public class ProjectNotificationsHelper
    {

        private static ApplicationDbContext db = new ApplicationDbContext();
        private static ProjectManagerHelper projectHelper = new ProjectManagerHelper();
        private static UserRolesHelper roleHelper = new UserRolesHelper();

        //    public void ManageNotifications(Project oldProject, Project newProject)
        //    {
        //        CreateAssignmentNotification(oldTicket, newTicket);
        //        CreateChangeNotification(oldProject, newProject);

        //    }

        //    public static void CreateAssignmentNotification(Project oldProject, Project newProject)
        //    {

        //        var oldProjectUsers = projectHelper.UsersOnProject(oldProject.Id).ToList();
        //        var newProjectUsers = projectHelper.UsersOnProject(newProject.Id).ToList();

        //        var noChange = (oldProjectUsers == newProjectUsers);
        //        var assignment = (string.IsNullOrEmpty(oldProject.AssignedToUserId));
        //        var unassignment = (string.IsNullOrEmpty(newProject.AssignedToUserId));

        //        if (noChange)
        //            return;

        //        if (assignment)
        //            GenerateAssignmentNotification(oldProject, newProject);
        //        else if (unassignment)
        //            GenerateUnAssignmentNotification(oldProject, newProject);
        //        else
        //        {
        //            GenerateAssignmentNotification(oldProject, newProject);
        //            GenerateUnAssignmentNotification(oldProject, newProject);
        //        }
        //    }

        //    private static void GenerateUnAssignmentNotification(Project oldProject, Project newProject)
        //    {


        //        var notification = new TicketNotification
        //        {
        //            Created = DateTime.Now,
        //            Subject = $"You were unassigned from {newProject.Name} on {DateTime.Now}",
        //            Read = false,
        //            RecipientId = oldProject.AssignedToUserId,
        //            SenderId = HttpContext.Current.User.Identity.GetUserId(),
        //            NotificationBody = $"Please acknowledge that you have read this notification by marking it as READ",
        //            TicketId = newProject.Id
        //        };

        //        db.TicketNotifications.Add(notification);
        //        db.SaveChanges();

        //    }

        //    private static void GenerateAssignmentNotification(Project oldProject, Project newProject)
        //    {
        //        var notification = new TicketNotification
        //        {
        //            Created = DateTime.Now,
        //            Subject = $"You were assigned to {newProject.Id} on {DateTime.Now}",
        //            Read = false,
        //            RecipientId = newProject.AssignedToUserId,
        //            SenderId = HttpContext.Current.User.Identity.GetUserId(),
        //            NotificationBody = $"Please acknowledge that you have read this notification",
        //            TicketId = newProject.Id
        //        };

        //        db.TicketNotifications.Add(notification);
        //        db.SaveChanges();

        //    }

        //    private void CreateChangeNotification(Project oldProject, Project newProject)
        //    {
        //        var messageBody = new StringBuilder();

        //        foreach (var property in WebConfigurationManager.AppSettings["TrackedTicketProperties"].Split(','))
        //        {
        //            var oldValue = Utilities.MakeReadable(property, oldProject.GetType().GetProperty(property).GetValue(oldProject, null).ToString());
        //            var newValue = Utilities.MakeReadable(property, newProject.GetType().GetProperty(property).GetValue(newProject, null).ToString());

        //            if (oldValue != newValue)
        //            {

        //                messageBody.AppendLine($"A change was made to Property: {property}.");
        //                messageBody.AppendLine($"The old value was: {oldValue.ToString()}");
        //                messageBody.AppendLine($"The new value is: {newValue.ToString()}");
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(messageBody.ToString()))
        //        {
        //            var message = new StringBuilder();
        //            message.AppendLine($"Changes were made to Ticket Id: {newProject.Id} on {DateTime.Now.ToString("MMM d, yyyy")}");
        //            message.AppendLine(messageBody.ToString());
        //            var senderId = HttpContext.Current.User.Identity.GetUserId();

        //            var notification = new ProjectNotifications
        //            {
        //                ProjectId = newProject.Id,
        //                Created = DateTime.Now,
        //                Subject = $"Ticket Id: {newProject.Id} has changed",
        //                RecipientId = oldProject.AssignedToUserId,
        //                SenderId = senderId,
        //                NotificationBody = message.ToString(),
        //                Read = false
        //            };

        //            db.TicketNotifications.Add(notification);
        //            db.SaveChanges();
        //        }
        //    }
        //    public int GetNewUserNotificationCount()
        //    {
        //        var userId = HttpContext.Current.User.Identity.GetUserId();
        //        return db.TicketNotifications.Where(t => t.RecipientId == userId && !t.Read).Count();
        //    }
        //    public int GetAllUserNotificationCount()
        //    {
        //        var userId = HttpContext.Current.User.Identity.GetUserId();
        //        return db.TicketNotifications.Where(t => t.RecipientId == userId).Count();
        //    }

        //    public List<TicketNotification> GetUnreadUserNotifications()
        //    {
        //        var userId = HttpContext.Current.User.Identity.GetUserId();
        //        return db.TicketNotifications.Where(t => t.RecipientId == userId && !t.Read).ToList();

        //    }

        //    public List<TicketNotification> GetAllUserNotifications()
        //    {
        //        var userId = HttpContext.Current.User.Identity.GetUserId();
        //        return db.TicketNotifications.ToList();

        //    }
    }
}