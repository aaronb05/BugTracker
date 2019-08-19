using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class TicketHelper
    {
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectManagerHelper projectHelper = new ProjectManagerHelper();

        private ApplicationDbContext db = new ApplicationDbContext();

        public List<string> UsersInRoleOnTicket(int ticketId, string roleName)
        {
            var people = new List<string>();

            foreach (var user in UsersOnTicket(ticketId).ToList())
            {
                if(roleHelper.IsUserInRole(user.Id, roleName))
                {
                    people.Add(user.Id);
                }

            }

            return people;
        }

        public ICollection<ApplicationUser> UsersOnTicket(int ticketId)
        {
            return db.Tickets.Find(ticketId).Project.Users.ToList();
        }
        public bool IsUserOnTicket(string userId, int ticketId)
        {
            
            var ticket = db.Tickets.Find(ticketId);
            var flag = ticket.Project.Users.Any(u => u.Id == userId);

            return (flag);

        }

        public ICollection<Ticket> ListUserTickets(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var tickets = user.Tickets.ToList();

            return tickets;
        }

        public void AddUserToTicket(string userId, int ticketId)
        {
           
                if (!IsUserOnTicket(userId, ticketId))
                {
                    Ticket tick = db.Tickets.Find(ticketId);
                    tick.AssignedToUserId = userId;
                    db.SaveChanges();
                }
            
        }

        public void RemoveUserFromTicket(string userId, int ticketId)
        {
            if (IsUserOnTicket(userId, ticketId))
            {
                Ticket tick = db.Tickets.Find(ticketId);
                var delUser = db.Users.Find(userId);

                tick.Project.Users.Remove(delUser);
                db.Entry(tick).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        public ICollection<ApplicationUser> UsersOnTicket(int ticketId, int projectId)
        {
            return db.Tickets.Find(ticketId).Project.Users;
        }

        public ICollection<ApplicationUser> UsersNotOnTicket(int ticketId)
        {
            return db.Users.Where(u => u.Tickets.All(t => t.Id != ticketId)).ToList();
        }

        public ICollection<ApplicationUser> UsersOnTicketByRole(int ticketId, string roleName)
        {
            var ticketUsers = new List<ApplicationUser>();
            var users = UsersOnTicket(ticketId);

            foreach(var user in users)
            {
                if (roleHelper.ListUserRoles(user.Id).FirstOrDefault() == roleName)
                {
                    ticketUsers.Add(user);
                }
            }

            return ticketUsers;
        }


      


    }
}