using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zinder.Models
{
    /*
     * ProfileModel id used for key reference in the Database
     */
    public class PostModel
    {
        [Key]
        public int ID { get; set; }

        public string Author { get; set; }
        public string Reciever { get; set; }
        public string Message { get; set; }

        public virtual ProfileModel Profile { get; set; }
    }
}