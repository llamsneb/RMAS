using System;
using System.Collections.Generic;

namespace RMAS.Models
{
    public partial class Room
    {
        public Room()
        {
            Event = new HashSet<Event>();
        }

        public int RoomNumber { get; set; }
        public string RoomType { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
