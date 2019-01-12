using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zinder.Models;

namespace Zinder.Controllers
{
    public class SearchController : Controller
    {
        // Method to get and list all the profiles
        public ActionResult Index()
        {
            var vm = new ProfileListViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var ctx = new ProfileDbContext();

                // Gets all the profiles and puts it in the variable "vm"
                vm = new ProfileListViewModel
                {
                    Profiles = ctx.Profiles.ToList()
                };
            }

            return View(vm);
        }




        public ActionResult UserProfile(string id)
        {
            var ctx = new ProfileDbContext();
            var profile = ctx.Profiles.FirstOrDefault(p => p.ID == id);

            return View(new ProfileViewModel
            {
                ID = profile?.ID,
                FirstName = profile?.FirstName,
                LastName = profile?.LastName,
                DateOfBirth = profile?.DateOfBirth,
                Description = profile?.Description
            });
        }
    }
}