namespace ApI.Request_Model
{
    public class ParkingSession
    {
        public Guid GarageId { get; set; }

        public Guid UserId { get; set; }

        public string LicensePlateNumber { get; set; }

        public string IPAddress { get; set; }
    }
}
