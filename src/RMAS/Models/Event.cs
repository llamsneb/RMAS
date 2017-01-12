using System;
using System.Collections.Generic;

namespace RMAS.Models
{
    public partial class Event
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int RoomNumber { get; set; }
        public string UserId { get; set; }

        public virtual Room RoomNumberNavigation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
