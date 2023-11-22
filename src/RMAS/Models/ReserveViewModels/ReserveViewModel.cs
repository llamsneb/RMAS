using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using RMAS.Models.Validation;
using RMAS.Models.SearchViewModels;
using RMAS.Models.BaseViewModels;

namespace RMAS.Models.ReserveViewModels
{
    [ValidateReserveTime]
    public class ReserveViewModel : BaseViewModel
    {
        public ReserveViewModel() 
        {
            Reservations = new List<Reservation>();
            Events = new List<Event>();
        }

        public SelectList? RoomTypes { get; set; }

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
        
        [ValidateReserveDate]
        public List<DateOnly> Dates { get; set; }

        [Required]
        public string EventName { get; set; }
        
        [Required]
        public TimeSpan BeginTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int RoomNumber { get; set; }


        public List<Event>? Events { get; set; }
    }
}
