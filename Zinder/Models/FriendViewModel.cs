﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinder.Models
{
    public class FriendViewModel
    {
        public string ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RequesterId { get; set; }
    }
}