using JiuJitsuWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JiuJitsuWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<User> Users => Set<User>();
        public DbSet<TimeTableModel> TimeTables => Set<TimeTableModel>();

        public DbSet<BookingModel> Bookings => Set<BookingModel>();

    }
}
