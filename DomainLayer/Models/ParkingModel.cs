using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ParkingModel
    {
        public Guid GarageId { get; set; }

        public int BookedParking { get; set; }
    }
}
