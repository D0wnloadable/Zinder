using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Zinder.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsApiController : ApiController
    {
        //[RoutePrefix("post/add")]
        [HttpGet]
        public void PostMessage(string recieverId, string text)
        {

        }
    }
}
