using BugTracker.Enumerations;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BugTracker.Helpers
{
    public class DecisionHelper
    {
        private static UserRolesHelper roleHelper = new UserRolesHelper();
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static ProjectManagerHelper projectHelper = new ProjectManagerHelper();
        public static bool TicketIsDetailIsViewableByUser(string userId, int ticketId)
        {
            //var userId = HttpContext.Current.User.Identity.GetUserId();
            var roleName = roleHelper.ListUserRoles(userId).FirstOrDefault();
            var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName);
            switch (systemRole)
            {
                case SystemRole.Admin:
                    break;
                case SystemRole.ProjectManager:
                    break;
                case SystemRole.Developer:
                    break;
                case SystemRole.Submitter:
                    break;
            }
            return true;
        }

        public static bool ProjectIsEditableByUser(Project project)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Developer":
                    return false;
                case "Submitter":
                    return false;
                case "Project Manager":
                    var listMyProjects = projectHelper.ListUserProjects(userId);
                    foreach (var myProject in listMyProjects)
                    {
                        if (myProject.Id == project.Id)
                            return true;
                       
                    }
                    return false;
                case "Admin":
                    return true;
                default:
                    return false;

            }

        }
        public static bool TicketIsEditableByUser(Ticket ticket)
        {

            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId;
                case "Submitter":
                    return ticket.OwnerUserId == userId;
                case "Project Manager":
                    var myProjects = projectHelper.ListUserProjects(userId);
                    foreach (var project in myProjects)
                    {
                        foreach (var projticket in project.Tickets)
                        {
                            if (projticket.Id == ticket.Id)
                                return true;
                        }
                    }
                    return false;
                case "Admin":
                    return true;
                default:
                    return false;

            }
        }
        public bool TicketTypeIsEditableByUser(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            return ticket.OwnerUserId == userId;
        }
        public bool TicketStatusIsEditableByUser(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            return ticket.OwnerUserId == userId;
        }
        public bool TicketPriorityIsEditableByUser(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            return ticket.OwnerUserId == userId;
        }
        public bool TicketTitleIsEditableByUser(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            return ticket.OwnerUserId == userId;
        }
        public bool TicketDescriptionIsEditableByUser(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            return ticket.OwnerUserId == userId;
        }
        public bool TicketAssignedToUserIdIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

    }
}