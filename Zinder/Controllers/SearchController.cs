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
        // GET: Search
        public ActionResult Index()
        {
            var ctx = new ProfileDbContext();

            var viewModel = new ProfileListViewModel
            {
                Profiles = ctx.Profiles.ToList()
            };

            return View(viewModel);
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