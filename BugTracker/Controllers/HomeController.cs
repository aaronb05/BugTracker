using BugTracker.Helpers;
using BugTracker.Models;
using BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        //[Authorize]
        public ActionResult DashBoard()
        {
            return View();
        }
        [Authorize]
        //GET
        public ActionResult UserProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = new UserProfileVM
            {
                Id = userId,
                AvatarUrl = db.Users.Find(userId).AvatarUrl
            };

            return View(user);
        }

        [HttpPost]
        public ActionResult UserProfile(UserProfileVM model, HttpPostedFileBase avatar)
        {
            var user = db.Users.Find(model.Id);

            if (ImageHelpers.IsWebFriendlyImage(avatar))
            {
                var fileName = Path.GetFileName(avatar.FileName);
                avatar.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), fileName));
                user.AvatarUrl = "/Avatars/" + fileName;
            }

            db.SaveChanges();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [AllowAnonymous]
        public ActionResult DemoUser()
        {
            return View();

        }
    }
}