using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
	public class AlpataProjeDbContext: DbContext
	{
        public AlpataProjeDbContext(DbContextOptions<AlpataProjeDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<UserMeeting> UserMeetings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserMeeting>()
				.HasKey(um => new { um.UserId, um.MeetingId });

			modelBuilder.Entity<UserMeeting>()
				.HasOne(um => um.User)
				.WithMany(u => u.UserMeetings)
				.HasForeignKey(um => um.UserId);

			modelBuilder.Entity<UserMeeting>()
				.HasOne(um => um.Meeting)
				.WithMany(m => m.UserMeetings)
				.HasForeignKey(um => um.MeetingId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
