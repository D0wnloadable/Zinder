using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zinder.Models
{
    public class ProfileViewModel
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "Please enter you First Name")]
        [StringLength(60, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        [StringLength(60, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Description { get; set; }

        public bool Exists { get; set; }
    }
}