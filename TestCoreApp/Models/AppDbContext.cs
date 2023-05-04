using Microsoft.EntityFrameworkCore;

namespace TestCoreApp.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
		{

		}
		public DbSet<Item> Items { get; set; }
		public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1, Name = "Select category" },
                new Category { Id = 2, Name = "Computer" },
                new Category { Id = 3, Name = "Mobile" },
                new Category { Id = 4, Name = "tablet" }
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}
