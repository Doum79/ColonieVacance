using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Provider.EntityFramework
{
    public class Context : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Structure> Structure { get; set; }
        public DbSet<Stay> Stay { get; set; }
        public DbSet<UserFavoriteStay> UserFavoriteStay { get; set; }
        public DbSet<UserFavoriteStructure> UserFavoriteStructure { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<StayActivity> StayActivity { get; set; }
        public DbSet<Thematic> Thematic { get; set; }
        public DbSet<StayThematic> StayThematic { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<StayTag> StayTag { get; set; }
        public DbSet<StayTeam> StayTeam { get; set; }
        public DbSet<StayEquipment> StayEquipment { get; set; }
        public DbSet<StayAccess> StayAccess { get; set; }
        public DbSet<StayPicture> StayPicture { get; set; }
        public DbSet<StayFurtherInformation> StayFurtherInformation { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}
