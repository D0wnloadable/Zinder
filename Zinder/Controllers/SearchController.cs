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
        /*
         * Method to list and search through users
         */
        public ActionResult Index(string firstname, string lastname)
        {
            List<ProfileModel> searchResult = new List<ProfileModel>();

            if (User.Identity.IsAuthenticated)
            {
                var ctx = new ZinderUserDbContext();

                // When the user loads the search page or leave search empty, only 5 users will be listed
                searchResult = ctx.Profiles.Take(5).ToList();

                if (!string.IsNullOrEmpty(firstname) || !string.IsNullOrEmpty(lastname))
                {
                    searchResult = ctx.Profiles.Where(p => p.FirstName.StartsWith(firstname)
                        && p.LastName.StartsWith(lastname)).ToList();
                }
            }

            return View(searchResult);
        }



        /*
         * Gets and views a full profile
         */
        public ActionResult UserProfile(string id)
        {
            var ctx = new ZinderUserDbContext();
            var profile = ctx.Profiles.FirstOrDefault(p => p.ID == id);

            return View(new ProfileViewModel
            {
                ID = profile?.ID,
                FirstName = profile?.FirstName,
                LastName = profile?.LastName,
                DateOfBirth = profile?.DateOfBirth,
                Description = profile?.Description,
                ImageUrl = profile?.ImageUrl
            });
        }
    }
}