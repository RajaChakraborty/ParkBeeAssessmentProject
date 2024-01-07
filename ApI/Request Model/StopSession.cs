namespace ApI.Request_Model
{
    public class StopSession
    {
        public Guid UserId { get; set; }

        public Guid GarageId { get; set; }

        public string IPAddress { get; set; }
    }
}
