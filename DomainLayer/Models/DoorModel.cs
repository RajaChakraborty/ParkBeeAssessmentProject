using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class DoorModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DoorType DoorType { get; set; }
    }
}
