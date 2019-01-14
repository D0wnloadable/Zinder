using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinder.Models
{
    public class PostViewModel
    {
        public int ID { get; set; }

        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string Reciever { get; set; }
        public string Message { get; set; }
    }
}