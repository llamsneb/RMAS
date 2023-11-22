using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMAS.Interfaces;
using RMAS.Models;
using RMAS.Models.CalendarViewModels;
using System.Data;

namespace RMAS.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRoomRepository _roomRepository;

        public CalendarController(IEventRepository eventRepository, IRoomRepository roomRepository)
        {
            _eventRepository = eventRepository;
            _roomRepository = roomRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Calendar()
        {
            CalendarViewModel model = new CalendarViewModel();
            model.RoomTypes = await GetRoomTypes();
            model.Events = await _eventRepository.GetEvents();
            return View(model);            
        }

        [HttpPost]
        public async Task<JsonResult> GetRoomNumbers(string roomType)
        {
            List<Room> rooms = await _roomRepository.GetRooms();
            List<int> RoomNumbers = rooms.Where(r => r.RoomType == roomType).Select(r => r.RoomNumber).ToList();
            return Json(RoomNumbers);
        }

        public async Task<SelectList> GetRoomTypes()
        {
            var rooms = await _roomRepository.GetRooms();
            return new SelectList(rooms.Select(r => r.RoomType).Distinct().ToList());
        }

        public async Task<List<CalendarViewModel.CalendarEvent>> GetEventsFullCalendar(int roomNumber, DateTime start, DateTime end)
        {
            if (roomNumber > 0)
            {
                DateOnly startDate = DateOnly.FromDateTime(start.Date);
                DateOnly endDate = DateOnly.FromDateTime(end.Date);
                var results =  await _eventRepository.GetEventsFullCalendar(roomNumber, startDate, endDate);

                return results.Select(e =>
                    new CalendarViewModel.CalendarEvent
                    {
                        Title = e.EventName,
                        Start = e.EventDate.ToDateTime(TimeOnly.FromTimeSpan(e.BeginTime)),
                        End = e.EventDate.ToDateTime(TimeOnly.FromTimeSpan(e.EndTime))
                    })
                    .Where(c => c.Start >= start && c.End <= end).ToList();
            }
            
            return new List<CalendarViewModel.CalendarEvent>();
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _eventRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
