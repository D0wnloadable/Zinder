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



        /*
         * Method to show the user it's friend list
         */
        public ActionResult FriendList()
        {
            var friendList = new List<FriendViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                var ctx = new ProfileDbContext();
                var currentUserId = User.Identity.GetUserId();
                var currentUserProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUserId);

                // Gets a list of profiles in which the current user is friend with
                var profileList = currentUserProfile.Friends.Where(f => f.IsFriend);

                foreach (var friend in profileList)
                {
                    // Gets one of the friends full profile
                    var friendProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friend.RequesterId);

                    var vm = new FriendViewModel
                    {
                        ID = friend.RecieverId,
                        FirstName = friendProfile.FirstName,
                        LastName = friendProfile.LastName,
                        RequesterId = friend.ID
                    };

                    // Adds the friend view model to the list of friends
                    friendList.Add(vm);

                }
            }

            return View(friendList);
        }

        

        /*
         * Method to send a friend request to a user
         */
        public ActionResult SendFriendRequest(string friendId)
        {
            var ctx = new ProfileDbContext();
            var currentUserId = User.Identity.GetUserId();

            // Gets the friends full profile
            var friendProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friendId);

            // Adds the friend model to the ICollection of friends
            friendProfile.Friends.Add(new FriendModel
            {
                IsFriend = false,
                RequesterId = currentUserId,
                RecieverId = friendId
            });

            ctx.SaveChanges();

            return RedirectToAction("FriendList");
        }
        
        
        
        /*
         * Method to show the user the page for friend requests
         */
        public ActionResult FriendRequestList()
        {
            var friendRequestList = new List<FriendViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                var ctx = new ProfileDbContext();
                var currentUser = User.Identity.GetUserId();
                var currentProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

                // Gets a list of profiles in which the current user is NOT friend with
                var profileList = currentProfile.Friends.Where(f => !f.IsFriend);

                foreach (var friend in profileList)
                {
                    // Gets the friends full profile
                    var friendsProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friend.RequesterId);

                    var vm = new FriendViewModel
                    {
                        ID = friend.RecieverId,
                        FirstName = friendsProfile.FirstName,
                        LastName = friendsProfile.LastName,
                        RequesterId = friend.ID

                    };
                    friendRequestList.Add(vm);
                }
            }

            return View(friendRequestList);
        }



        /*
         * Method to accept a friend request
         */
        public ActionResult AcceptFriendRequest(int id)
        {
            var ctx = new ProfileDbContext();
            var currentUserId = User.Identity.GetUserId();
            var currentProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUserId);

            // Gets the requester friend model and sets Isfriend
            var friend = currentProfile.Friends.FirstOrDefault(f => f.ID == id);
            friend.IsFriend = true;
            
            var requesterProfile = ctx.Profiles.FirstOrDefault(p => p.ID == friend.RequesterId);
            
            // Updates the database
            requesterProfile.Friends.Add(new FriendModel
            {
                IsFriend = true,
                RequesterId = friend.RequesterId,
                RecieverId = currentUserId
            });

            ctx.SaveChanges();

            return RedirectToAction("FriendList");
        }



        /*
         * Method to deny a friend request
         */
        public ActionResult DenyFriendRequest(int id)
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = ctx.Profiles.FirstOrDefault(p => p.ID == currentUser);

            var request = currentProfile.Friends.FirstOrDefault(r => r.ID == id);

            var otherProfile = ctx.Profiles.FirstOrDefault(p => p.ID == request.RequesterId);
            var otherRequest = otherProfile.Friends.FirstOrDefault(p => p.RequesterId == currentUser);
            if (otherRequest != null)
            {
                otherProfile.Friends.Remove(otherRequest);
            }
            currentProfile.Friends.Remove(request);
            ctx.SaveChanges();
            return RedirectToAction("FriendList");
        }
    }
}