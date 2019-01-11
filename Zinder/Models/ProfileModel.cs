using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zinder.Models
{
    public class ProfileModel
    {
        [Key]
        public string ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Description { get; set; }

        public bool Exists { get; set; }

        public virtual ICollection<FriendModel> Friends { get; set; }

        //  public string ImageName { get; set; }

    }

    public class ProfileDbContext : DbContext
    {
        // Constructor
        public ProfileDbContext() : base("ProfilesDb") { }

        public DbSet<ProfileModel> Profiles { get; set; }
        public DbSet<FriendModel> Friends { get; set; }
    }
}