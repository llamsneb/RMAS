using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RMAS.Models.ReserveViewModels
{
    public class ReserveViewModel
    {
        public List<SelectListItem>
        RoomTypes
        { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Conference", Text = "Conference" },
            new SelectListItem { Value = "Lab", Text = "Lab" },
            new SelectListItem { Value = "Class", Text = "Class"  },
            new SelectListItem { Value = "Office", Text = "Office"  }
        };

        public List<SelectListItem>
        DaysOfWeek
        { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Sun" },
            new SelectListItem { Value = "", Text = "Mon" },
            new SelectListItem { Value = "", Text = "Tues"  },
            new SelectListItem { Value = "", Text = "Wed"  },
            new SelectListItem { Value = "", Text = "Thur"  },
            new SelectListItem { Value = "", Text = "Fri"  },
            new SelectListItem { Value = "", Text = "Sat"  }
        };

        public List<SelectListItem>
        BeginTimeList
        { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "7:00", Text = "7:00am" },
            new SelectListItem { Value = "8:00", Text = "8:00am" },
            new SelectListItem { Value = "9:00", Text = "9:00am" },
            new SelectListItem { Value = "10:00", Text = "10:00am" },
            new SelectListItem { Value = "11:00", Text = "11:00am" },
            new SelectListItem { Value = "12:00", Text = "12:00pm" },
            new SelectListItem { Value = "13:00", Text = "1:00pm" },
            new SelectListItem { Value = "14:00", Text = "2:00pm" },
            new SelectListItem { Value = "15:00", Text = "3:00pm" },
            new SelectListItem { Value = "16:00", Text = "4:00pm" },
            new SelectListItem { Value = "17:00", Text = "5:00pm" },
            new SelectListItem { Value = "18:00", Text = "6:00pm" },
            new SelectListItem { Value = "19:00", Text = "7:00pm" }
        };

        public List<SelectListItem>
        EndTimeList
        { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "8:00", Text = "8:00am" },
            new SelectListItem { Value = "9:00", Text = "9:00am" },
            new SelectListItem { Value = "10:00", Text = "10:00am" },
            new SelectListItem { Value = "11:00", Text = "11:00am" },
            new SelectListItem { Value = "12:00", Text = "12:00pm" },
            new SelectListItem { Value = "13:00", Text = "1:00pm" },
            new SelectListItem { Value = "14:00", Text = "2:00pm" },
            new SelectListItem { Value = "15:00", Text = "3:00pm" },
            new SelectListItem { Value = "16:00", Text = "4:00pm" },
            new SelectListItem { Value = "17:00", Text = "5:00pm" },
            new SelectListItem { Value = "18:00", Text = "6:00pm" },
            new SelectListItem { Value = "19:00", Text = "7:00pm" },
            new SelectListItem { Value = "20:00", Text = "8:00pm" }
        };

        public List<DateTime> Dates { get; set; }

        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int RoomNumber { get; set; }
    }
}
