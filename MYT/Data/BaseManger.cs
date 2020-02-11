using MYT.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data
{
    public class BaseManger
    {
        public DateTime TimeOfCreate { get; set; }
        public int CreatorId { get; set; }
        public DateTime LastChanged { get; set; }
        public bool IsDone { get; set; }
        public bool IsDeleted { get; set; }

        public UserProfile UserId { get; set; }
    }
}
