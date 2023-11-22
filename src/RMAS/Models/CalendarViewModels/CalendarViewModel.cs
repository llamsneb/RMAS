using Microsoft.AspNetCore.Mvc.Rendering;

namespace RMAS.Models.CalendarViewModels
{
    public class CalendarViewModel
    {
        public List<Event>? Events { get; set; }
        public SelectList? RoomTypes { get; set; }
        public int RoomNumber { get; set; }
        public List<CalendarEvent>? CalendarEvents { get; set; } = new List<CalendarEvent>();
        public class CalendarEvent { 
            public string Title { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; } 
        }

    }
}
