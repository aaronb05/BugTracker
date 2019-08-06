using BugTracker.Helpers;
using BugTracker.Models;
using BugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectManagerHelper projectHelper = new ProjectManagerHelper();
        // GET: Admin

        //[Authorize(Roles = "Admin")]
        public ActionResult UserIndex()
        {
            var users = db.Users.Select(u => new UserProfileVM
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email

            }).ToList();

            return View(users);
        }

        //GET
        public ActionResult ManageUserRole(string userId)
        {
            //How to load up drop down list with role info.
            //SelectList(List of data target, column that will comm selection to post, 
            //colmn that we show the user inside the control
            //if they already occupy role, shows this instead of nothing
            var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.UserId = userId;
            ViewBag.UserRole = new SelectList(db.Roles.ToList(), "Name", "Name", currentRole);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRole(string userId, string UserRole)
        {
            //user UserRolesHelper class
            //remove user from all roles
            foreach (var role in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }
            //if incoming role is not null, assign to role
            if (!string.IsNullOrEmpty(UserRole))
            {
                roleHelper.AddUserToRole(userId, UserRole);
            }
                

            return RedirectToAction("UserIndex");
        }
            //GET
        public ActionResult ManageRoles()
        {
            var users = db.Users.Select(u => new UserProfileVM
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email

            }).ToList();

            ViewBag.Users = new MultiSelectList(db.Users.ToList(), "Id", "Email");
            ViewBag.RoleName = new SelectList(db.Roles.ToList(), "Name", "Name");

            return View(users);
        }

        [HttpPost]
        public ActionResult ManageRoles(List<string> users, string roleName)
        {
            if (users != null)
            {            
                foreach (var userId in users)
                {
                    foreach(var role in roleHelper.ListUserRoles(userId))
                    { 
                        roleHelper.RemoveUserFromRole(userId, role);
                    }
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        roleHelper.AddUserToRole(userId, roleName);
                    }
                }
            }
            return RedirectToAction("ManageRoles");
        }

        public ActionResult ManageUserProjects(string userId)
        {
            var myProjects = projectHelper.ListUserProjects(userId).Select(p => p.Id);
           
            ViewBag.Projects = new MultiSelectList(db.Projects.ToList(), "Id", "Name", myProjects);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserProjects(List<int>projects, string userId)
        {
            foreach (var project in projectHelper.ListUserProjects(userId).ToList())
            {
                projectHelper.RemoveUserFromProject(userId, project.Id);

            }
            if (projects != null)
            {
                foreach (var projectId in projects)
                {
                    projectHelper.AddUserToProject(userId, projectId);
                }
            }
            
            return RedirectToAction("ManageUserProjects");
        }

        [HttpPost]
        public ActionResult ManageProjectUsers(int projectId, List<string> ProjectManagers, List<string> Developers, List<string> Submitters)
        {
            foreach(var user in projectHelper.UsersOnProject(projectId).ToList())
            {
                projectHelper.RemoveUserFromProject(user.Id, projectId);

            }
            if (ProjectManagers != null)
            {
                foreach (var projectManagerId in ProjectManagers)
                {
                    projectHelper.AddUserToProject(projectManagerId, projectId);
                }
            }
            if (Developers != null)
            {
                foreach (var developerId in Developers)
                {
                    projectHelper.AddUserToProject(developerId, projectId);
                }
            }
            if (Submitters != null)
            {
                foreach (var submitterId in Submitters)
                {
                    projectHelper.AddUserToProject(submitterId, projectId);
                }
            }

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }
        public ActionResult ManageUsers()
        {
            return View();
        }
    }
}