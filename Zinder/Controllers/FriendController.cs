using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zinder.Models;

namespace Zinder.Controllers
{
    public class FriendController : Controller
    {
        public ActionResult ListProfiles()
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            var listOfProfilesInFriendList = currentUserProfile.Friends.Where(f => !f.IsFriend);
            var listOfFriends = new List<FriendViewModel>();
            foreach (var friend in listOfProfilesInFriendList)
            {
                var friendsProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friend.RequesterId);
                var friendModel = new FriendViewModel
                {
                    ID = friend.RecieverId,
                    FirstName = friendsProfile.FirstName,
                    LastName = friendsProfile.LastName,
                    RequesterId = friend.ID
                };
                listOfFriends.Add(friendModel);
            }

            return View(listOfFriends);
        }




        // GET: Friend
        public ActionResult SendFriendRequest(string friendId)
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            //var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            var friendProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friendId);

            friendProfile.Friends.Add(new FriendModel
            {
                IsFriend = false,
                RequesterId = currentUser,
                RecieverId = friendId
            });

            ctx.SaveChanges();

            return RedirectToAction("FriendList");
        }




        public ActionResult FriendRequestUser(string userId)
        {
            var ctx = new ProfileDbContext();
            var profile = ctx.Profiles.FirstOrDefault(p => p.ID == userId);
            return View(new ProfileViewModel
            {
                ID = profile?.ID,
                FirstName = profile?.FirstName,
                LastName = profile?.LastName,
                DateOfBirth = profile?.DateOfBirth,
                Description = profile?.Description
            });
        }
        



        /*
         * WORKS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
         */
        public ActionResult FriendList()
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            var listOfProfilesInFriendList = currentProfile.Friends.Where(f => f.IsFriend);
            var listOfFriends = new List<FriendViewModel>();

            foreach (var friend in listOfProfilesInFriendList)
            {
                var friendsProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friend.RequesterId);
                var viewModel = new FriendViewModel
                {
                    ID = friend.RecieverId,
                    FirstName = friendsProfile.FirstName,
                    LastName = friendsProfile.LastName,
                    RequesterId = friend.ID
                };
                listOfFriends.Add(viewModel);

            }

            return View(listOfFriends);
        }



        /*
         * WOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOORKS!!!!!!!!!
         */
        public ActionResult FriendRequestList()
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            var listOfProfilesInFriendList = currentProfile.Friends.Where(f => !f.IsFriend);
            var listOfFriends = new List<FriendViewModel>();
            foreach (var friend in listOfProfilesInFriendList)
            {
                var friendsProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friend.RequesterId);
                var friendModel = new FriendViewModel
                {
                    ID = friend.RecieverId,
                    FirstName = friendsProfile.FirstName,
                    LastName = friendsProfile.LastName,
                    RequesterId = friend.ID

                };
                listOfFriends.Add(friendModel);

            }

            return View(listOfFriends);
        }



        public ActionResult AcceptFriendRequest(int id)
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            var request = currentProfile.Friends.FirstOrDefault(r => r.ID == id);

            request.IsFriend = true;

            var senderProfile = ctx.Profiles.FirstOrDefault(p => p.ID == request.RequesterId);


            senderProfile.Friends.Add(new FriendModel
            {
                IsFriend = true,
                RequesterId = request.RequesterId,
                RecieverId = currentUser
            });

            ctx.SaveChanges();

            return RedirectToAction("FriendList");
        }
    }
}