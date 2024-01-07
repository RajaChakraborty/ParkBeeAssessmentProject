using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DatabaseContext
{

    public class Door
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DoorType DoorType { get; set; }
    }
}