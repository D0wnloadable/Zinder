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
        /*
         * HashSet and ICollection is used for foreign key reference in the tables.
         * ICollection is used to access method as "Add".
         */
        public ProfileModel()
        {
            this.Friends = new HashSet<FriendModel>();
            this.Posts = new HashSet<PostModel>();
        }

        [Key]
        public string ID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool Exists { get; set; }

        public virtual ICollection<FriendModel> Friends { get; set; }
        public virtual ICollection<PostModel> Posts { get; set; }
    }

    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext() : base("ProfilesDb")
        {
        }

        public DbSet<ProfileModel> Profiles { get; set; }
        public DbSet<FriendModel> Friends { get; set; }
        public DbSet<PostModel> Posts { get; set; }
    }
}