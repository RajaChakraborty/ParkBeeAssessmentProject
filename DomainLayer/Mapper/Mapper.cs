using DomainLayer.Models;
using RepositoryLayer.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Mapper
{
    public static class Mapper
    {
        public static List<GarageModel> ToDomainModel(this List<Garage> garages)
        {
            List<GarageModel> garageModels = new();


            garages.ForEach(g =>
            {
                garageModels.Add(new GarageModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Doors = g.Doors.ToDomainModel(),
                    TotalParkingSpots = g.TotalParkingSpots
                });
            });

            return garageModels;
        }

        public static List<DoorModel> ToDomainModel(this List<Door> doors)
        {
            List<DoorModel> doorsModel = new();

            doors.ForEach(door =>
            {
                doorsModel.Add(new DoorModel
                {
                    Id = door.Id,
                    Description = door.Description,
                    DoorType = (Models.DoorType)door.DoorType
                });
            });

            return doorsModel;
        }

        public static GarageModel ToDomainModel(this Garage garage)
        {
            return new GarageModel
            {
                Id = garage.Id,
                Name = garage.Name,
                Doors = garage.Doors.ToDomainModel(),
                TotalParkingSpots = garage.TotalParkingSpots
            };
        }

        public static ParkingModel ToDomainModel(this Parking parking)
        {
            return new ParkingModel
            {
                BookedParking = parking.BookedParking,
                GarageId = parking.GarageId,
            };
        }

        public static List<UserModel> ToDomainModel(this List<User> users)
        {
            List<UserModel> userModels = new();

            foreach (var user in users)
            {
                userModels.Add(new UserModel
                {
                    Id = user.Id,
                    PartnerId = user.PartnerId
                });
            }

            return userModels;
        }
    }
}
