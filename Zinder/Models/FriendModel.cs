using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zinder.Models
{
    public class FriendModel
    {
        [Key]
        public int ID { get; set; }

        public bool IsFriend { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }

        public virtual ProfileModel Profile { get; set; }
    }
}