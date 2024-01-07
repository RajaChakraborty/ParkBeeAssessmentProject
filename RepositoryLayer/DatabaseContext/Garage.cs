using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DatabaseContext
{

    public class Garage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int TotalParkingSpots { get; set; }

        public List<Door> Doors { get; set; }
    }
}