using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DatabaseContext
{
    public class Parking
    {
        [Key]
        public Guid GarageId { get; set; }

        public int BookedParking { get; set; }
    }
}
