using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class GarageModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int TotalParkingSpots { get; set; }

        public List<DoorModel> Doors { get; set; }
    }
}
