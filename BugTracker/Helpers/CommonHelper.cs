using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public abstract class CommonHelper
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        protected UserRolesHelper roleHelper = new UserRolesHelper();
        protected ProjectManagerHelper projectHelper = new ProjectManagerHelper();
        protected ApplicationUser CurrentRole = null;
        //protected System.CurrentRole


    }
}