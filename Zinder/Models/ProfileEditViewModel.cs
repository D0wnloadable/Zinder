using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zinder.Models
{
    /*
     * The view model for when editing the user profile
     */
    public class ProfileEditViewModel
    {
        public string ID { get; set; }

        [StringLength(60, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(60, MinimumLength = 1)]
        public string LastName { get; set; }

        [StringLength(200, MinimumLength = 1)]
        public string Description { get; set; }
    }
}