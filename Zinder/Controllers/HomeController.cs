using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zinder.Models;

namespace Zinder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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


        /*
         * Method to view 3 profiles on the home page
         */
        public ActionResult ViewProfiles()
        {
            var list = new ProfileListViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var ctx = new ZinderUserDbContext();

                list = new ProfileListViewModel
                {
                    Profiles = ctx.Profiles.Take(3).ToList()
                };
            }

            return PartialView(list);
        }
    }
}