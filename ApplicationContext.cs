using Microsoft.EntityFrameworkCore;

namespace Login
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserData> UserDatas { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().HasData(
                    new UserData
                    {
                        Id = 1,
                        UserName = "user",
                        PasswordHash = "d74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1",
                        Role = Roles.User,
                        LastFailedAttemptDate = null
                    }); ;
            modelBuilder.Entity<UserData>().HasData(
                    new UserData { Id = 2, UserName = "manager", 
                        PasswordHash = "d74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1",
                        Role = Roles.Manager,
                        LastFailedAttemptDate = null
                    });
        }
    }
}
