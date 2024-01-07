using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.DatabaseContext
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext()
        {
        }

        public ParkingDbContext(DbContextOptions<ParkingDbContext> options)
        : base(options)
        {
        }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Parking> Parkings { get; set; }

        public DbSet<UserSession> UserSessions { get; set; }
    }
}