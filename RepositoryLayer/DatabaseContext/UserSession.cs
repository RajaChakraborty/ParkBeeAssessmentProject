using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DatabaseContext
{
    public class UserSession
    {
        public Guid UserId { get; set; }

        [Key]
        public Guid SessionId { get; set; }
    }
}
