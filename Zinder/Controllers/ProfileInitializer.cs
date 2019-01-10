using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinder.Models;

namespace Zinder.Controllers
{
    public class ProfileInitializer
    {
        public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProfileDbContext>
        {
            protected override void Seed(ProfileDbContext ctx)
            {
                var profiles = new List<ProfileModel>
                {
                    new ProfileModel{ FirstName="Hugh", LastName="Mungus", DateOfBirth=DateTime.Parse("1969-04-20"), Description="Hugh Mungus Waht?" },
                    new ProfileModel{ FirstName="Felix", LastName="Kjellberg", DateOfBirth=DateTime.Parse("1969-11-05"), Description="But can you do dis?" }
                };

                profiles.ForEach(p => ctx.Profiles.Add(p));
                ctx.SaveChanges();
            }
        }
    }
}