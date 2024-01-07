using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class GarageParkingModel
    {
        public Guid GarageId { get; set; }

        public string GarageName { get; set; }

        public int AvailableParkingSpots { get; set; }
    }
}
