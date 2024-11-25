using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersApp.Models;

namespace UsersApp.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships and constraints
            modelBuilder.Entity<Flashcard>()
                .HasMany(f => f.Questions)
                .WithOne()
                .HasForeignKey(q => q.FlashcardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
