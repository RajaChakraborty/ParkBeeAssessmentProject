namespace ApI.ViewModel
{
    public class GarageVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int TotalParkingSpots { get; set; }

        public List<DoorVM> Doors { get; set; }
    }
}
