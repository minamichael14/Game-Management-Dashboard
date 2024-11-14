using GameZone.Models;

namespace GameZone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) 
            :base(options)
        {
        }
        public ApplicationDbContext():base()
        {
            
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category{Id = 1, Name="Sports"},
                    new Category{Id = 2, Name="Action"},
                    new Category{Id = 3, Name="Adventure"},
                    new Category{Id = 4, Name="Racing"},
                    new Category{Id = 5, Name="Fighting"},
                    new Category{Id = 6,  Name="Film"}
                }
                );

            modelBuilder.Entity<Device>()
                .HasData(new Device[]
                {
                    new(){Id = 1, Name="Playtation", Icon="bi bi-playtation"},
                    new(){Id = 2, Name="xbox", Icon="bi bi-xbox"},
                    new(){Id = 3, Name="Minecraft Switch", Icon="bi bi-nintendo-switch"},
                    new(){Id = 4, Name="PC", Icon="bi bi-PC-Display"}
                }
                );

            modelBuilder.Entity<GameDevice>()
                .HasKey(e => new {e.GameId , e.DeviceId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
    