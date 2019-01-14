using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Zinder.Models;

namespace Zinder.Controllers
{
    /*
     * !!! OBS !!!
     * Det går INTE att posta eller se inlägg ännu
     * 
     * rip
     */
    [RoutePrefix("api/posts")]
    public class PostsApiController : ApiController
    {
        [Route("posts/add")]
        [HttpGet]
        public void PostMessage(string recieverId, string text)
        {
            var ctx = new ZinderUserDbContext();
            var currentUserId = User.Identity.GetUserId();
            var recieverProfile = ctx.Profiles.FirstOrDefault(p => p.ID == recieverId);

            recieverProfile.Posts.Add(new PostModel {
                Author = currentUserId,
                Reciever = recieverId,
                Message = text
            });

            ctx.SaveChanges();
        }


        [HttpGet]
        [Route("posts/list")]
        public PostViewModel ViewMessages(string recieverId)
        {
            var ctx = new ZinderUserDbContext();
            var currentUserId = User.Identity.GetUserId();

            var vm = new PostViewModel();

            return vm;
        }
    }
}
