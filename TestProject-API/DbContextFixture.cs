using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject_API
{
    public class DbContextFixture : IDisposable
    {
        public ParkingDbContext DbContext { get; private set; }

        public DbContextFixture()
        {

            var options = new DbContextOptionsBuilder<ParkingDbContext>()
                            .UseInMemoryDatabase(databaseName: "parkbee")
                            .Options;
            DbContext = new ParkingDbContext(options);
            SeedDatabase();


        }

        public void Dispose()
        {
            // Clean up DbContext resources, if needed
            DbContext.Dispose();
        }

        private void SeedDatabase()
        {
            var garages = GetGaragesSeedData();
            DbContext.Garages.AddRange(Enumerable.Range(0, 3).Select(g => garages[g]));

            DbContext.Users.AddRange(Enumerable.Range(0, 20).Select(x => new User()
            {
                Id = Guid.NewGuid(),
                PartnerId = "partner-1"
            }));

            DbContext.SaveChanges();
        }

        private List<Garage> GetGaragesSeedData()
        {
            List<Garage> garages = new();

            var garageObj1 = new Garage
            {
                Id = Guid.Parse("0f6d8372-ad6f-4d25-b636-df2d1088b95c"),
                Name = $"Garage {Guid.NewGuid()}",
                TotalParkingSpots = 1,
                Doors = new List<Door>()
                {
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Entry
                    },
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Exit
                    },
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Pedestrian
                    }
                }
            };

            var garageObj2 = new Garage
            {
                Id = Guid.Parse("b46f25c5-f21f-402f-9542-cdca2e5d74fd"),
                Name = $"Garage {Guid.NewGuid()}",
                TotalParkingSpots = 2,
                Doors = new List<Door>()
                {
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Entry
                    },
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Exit
                    },
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Pedestrian
                    }
                }
            };

            var garageObj3 = new Garage
            {
                Id = Guid.Parse("2b56d301-7e7e-4487-b984-67cbc6e88f1b"),
                Name = $"Garage {Guid.NewGuid()}",
                TotalParkingSpots = 3,
                Doors = new List<Door>()
                {
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Entry
                    },
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Exit
                    },
                    new Door
                    {
                        Id = Guid.NewGuid(),
                        Description = $"Door {Guid.NewGuid()}",
                        DoorType = DoorType.Pedestrian
                    }
                }
            };

            garages.Add(garageObj1);
            garages.Add(garageObj2);
            garages.Add(garageObj3);

            return garages;
        }
    }

    [CollectionDefinition("SharedDbContext")]
    public class DbContextCollection : ICollectionFixture<DbContextFixture>
    {
        // This class has no code, and serves only as an attribute mark for the Collection
    }
}
