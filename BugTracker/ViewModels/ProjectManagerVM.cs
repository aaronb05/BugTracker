using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class ProjectManagerVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime Created { get; set; }
        public string ProjectManager { get; set; }
        public string ProjectDeveloper { get; set; }
        public string ProjectSubmitter { get; set; }
        

    }
}