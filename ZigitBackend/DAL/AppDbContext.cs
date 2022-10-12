using DAL.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new PersonalDetailsConfiguration().Configure(modelBuilder.Entity<PersonalDetails>());

            modelBuilder.Entity<User>()
                    .HasOne(u => u.PersonalDetails).WithOne()
                    .HasForeignKey<PersonalDetails>(e => e.UserId);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                Password = "Aa123456",
            });

            modelBuilder.Entity<PersonalDetails>().HasData(new PersonalDetails()
            {
                UserId = 1,
                Team = "Developers",
                Avatar = "https://avatarfiles.alphacoders.com/164/thumb-164632.jpg",
                JoinedAt = new DateTime(2018, 10, 1),
                Name = "Test Test"
            });

            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 1,
                ProjectGuid = Guid.NewGuid(),
                Name = "Backend Project",
                Score = 88,
                DurationInDays = 35,
                BugsCount = 74,
                MadeDeadline = false,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 2,
                ProjectGuid = Guid.NewGuid(),
                Name = "Design Project",
                Score = 68,
                DurationInDays = 55,
                BugsCount = 52,
                MadeDeadline = false,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 3,
                ProjectGuid = Guid.NewGuid(),
                Name = "Frontend Project",
                Score = 99,
                DurationInDays = 51,
                BugsCount = 32,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 4,
                ProjectGuid = Guid.NewGuid(),
                Name = "Design Project",
                Score = 97,
                DurationInDays = 68,
                BugsCount = 42,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 5,
                ProjectGuid = Guid.NewGuid(),
                Name = "Backend Project",
                Score = 97,
                DurationInDays = 66,
                BugsCount = 64,
                MadeDeadline = false,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 6,
                ProjectGuid = Guid.NewGuid(),
                Name = "Fullstack Project",
                Score = 79,
                DurationInDays = 61,
                BugsCount = 63,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 7,
                ProjectGuid = Guid.NewGuid(),
                Name = "Backend Project",
                Score = 66,
                DurationInDays = 62,
                BugsCount = 50,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 8,
                ProjectGuid = Guid.NewGuid(),
                Name = "Backend Project",
                Score = 79,
                DurationInDays = 44,
                BugsCount = 72,
                MadeDeadline = false,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 9,
                ProjectGuid = Guid.NewGuid(),
                Name = "Backend Project",
                Score = 93,
                DurationInDays = 66,
                BugsCount = 72,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 10,
                ProjectGuid = Guid.NewGuid(),
                Name = "Design Project",
                Score = 100,
                DurationInDays = 39,
                BugsCount = 47,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 11,
                ProjectGuid = Guid.NewGuid(),
                Name = "Design Project",
                Score = 87,
                DurationInDays = 68,
                BugsCount = 56,
                MadeDeadline = false,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 12,
                ProjectGuid = Guid.NewGuid(),
                Name = "Fullstack Project",
                Score = 76,
                DurationInDays = 36,
                BugsCount = 73,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 13,
                ProjectGuid = Guid.NewGuid(),
                Name = "Backend Project",
                Score = 90,
                DurationInDays = 36,
                BugsCount = 34,
                MadeDeadline = true,
                UserId = 1,
            });
            modelBuilder.Entity<Project>().HasData(new Project()
            {
                Id = 14,
                ProjectGuid = Guid.NewGuid(),
                Name = "Design Project",
                Score = 95,
                DurationInDays = 59,
                BugsCount = 65,
                MadeDeadline = false,
                UserId = 1,
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
