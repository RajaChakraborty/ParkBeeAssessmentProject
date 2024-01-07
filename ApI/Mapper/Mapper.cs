using ApI.ViewModel;
using DomainLayer.Models;

namespace ApI.Mapper
{
    public static class Mapper
    {
        public static List<GarageVM> ToViewModel(this List<GarageModel> garages)
        {
            List<GarageVM> garageViewModels = new();


            garages.ForEach(g =>
            {
                garageViewModels.Add(new GarageVM
                {
                    Id = g.Id,
                    Name = g.Name,
                    Doors = g.Doors.ToViewModel(),
                    TotalParkingSpots = g.TotalParkingSpots
                });
            });

            return garageViewModels;
        }

        public static List<DoorVM> ToViewModel(this List<DoorModel> doors)
        {
            List<DoorVM> doorsViewModel = new();

            doors.ForEach(door =>
            {
                doorsViewModel.Add(new DoorVM
                {
                    Id = door.Id,
                    Description = door.Description,
                    DoorType = door.DoorType.ToString()
                });
            });

            return doorsViewModel;
        }

        public static GarageVM ToViewModel(this GarageModel garage)
        {
            return new GarageVM
            {
                Id = garage.Id,
                Name = garage.Name,
                Doors = garage.Doors.ToViewModel(),
                TotalParkingSpots = garage.TotalParkingSpots
            };
        }

        public static AvailableParkingSpotsVM? ToViewModel(this GarageParkingModel garage)
        {
            if (garage != null)
            {
                return new AvailableParkingSpotsVM
                {
                    GarageId = garage.GarageId,
                    GarageName = garage.GarageName,
                    AvailableParkingSpots = garage.AvailableParkingSpots
                };
            }

            return null;
        }

        public static List<UserVM> ToViewModel(this List<UserModel> users)
        {
            List<UserVM> usersVM = new();

            foreach (var user in users)
            {
                usersVM.Add(new UserVM
                {
                    Id = user.Id,
                    PartnerId = user.PartnerId
                });
            }

            return usersVM;
        }
    }
}
