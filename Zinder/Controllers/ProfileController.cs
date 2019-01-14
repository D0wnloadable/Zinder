using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zinder.Models;

namespace Zinder.Controllers
{
    public class ProfileController : Controller
    {
        /*
         * Method to view the current user profile
         */
        public ActionResult Index()
        {
            var ctx = new ZinderUserDbContext();
            var currentUserId = User.Identity.GetUserId();
            var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUserId);

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
                ImageUrl = currentUserProfile?.ImageUrl,
                Exists = exists
            });
        }



        /*
         * Method to create a new profile if the current user profile doesn't exist
         */
        [HttpPost]
        public ActionResult CreateProfile(ProfileViewModel model, HttpPostedFileBase img)
        {
            var ctx = new ZinderUserDbContext();
            var currentUserId = User.Identity.GetUserId();
            var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUserId);

            // Adds the user if current user profile is not found
            if (currentUserProfile == null)
            {
                // Sets a placeholder image if the user decides to not upload an image
                var imageUrl = "/Images/Placeholder-Zinder.jpg";

                if (img != null && img.ContentLength > 0)
                {
                    string imgName = Path.GetFileName(img.FileName);
                    string url = Path.Combine(Server.MapPath("~/Images"), imgName);
                    img.SaveAs(url);
                    imageUrl = "/Images/" + imgName;
                }

                
                var dateTimeValue = model.DateOfBirth.Value;

                // Sets a default DateTime if the user input if below year 1753
                if (dateTimeValue.Year <= 1753)
                {
                    dateTimeValue = DateTime.ParseExact("20/04/1969 00:00:00", "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture);
                }

                ctx.Profiles.Add(new ProfileModel
                {
                    ID = currentUserId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = dateTimeValue,
                    Description = model.Description,
                    ImageUrl = imageUrl
                });
            }

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }



        /*
         * Method to edit a profile.
         * This method allows a user to edit only one or all of the profile values.
         */
        public ActionResult EditProfile(ProfileEditViewModel model)
        {
            var ctx = new ZinderUserDbContext();
            var currentUserId = User.Identity.GetUserId();
            var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUserId);

            currentUserProfile.FirstName = model.FirstName ?? currentUserProfile.FirstName;
            currentUserProfile.LastName = model.LastName ?? currentUserProfile.LastName;
            currentUserProfile.Description = model.Description ?? currentUserProfile.Description;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}