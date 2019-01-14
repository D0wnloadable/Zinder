using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zinder.Models;

namespace Zinder.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            var exists = false;

            if (currentUserProfile != null)
            {
                exists = true;
            }

            return View(new ProfileViewModel
            {
                FirstName = currentUserProfile?.FirstName,
                LastName = currentUserProfile?.LastName,
                DateOfBirth = currentUserProfile?.DateOfBirth,
                Description = currentUserProfile?.Description,
                // ImageName = currentProfile?.ImageName,
                Exists = exists
            });
        }



        [HttpPost]
        public ActionResult CreateProfile(ProfileViewModel model)
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            // Adds the user if current user profile is not found
            if (currentUserProfile == null)
            {
                var yearDefaultValue = model.DateOfBirth.Value;

                if (yearDefaultValue.Year <= 1753)
                {
                    yearDefaultValue = DateTime.ParseExact("20/04/1969 00:00:00", "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture);
                }

                ctx.Profiles.Add(new ProfileModel
                {
                    ID = currentUser,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = yearDefaultValue,
                    Description = model.Description
                });
            }
            
            // Edits the user if current user profile is found
            else
            {
                currentUserProfile.FirstName = model.FirstName ?? currentUserProfile.FirstName;
                currentUserProfile.LastName = model.LastName ?? currentUserProfile.LastName;
                currentUserProfile.Description = model.Description ?? currentUserProfile.Description;
            }

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}