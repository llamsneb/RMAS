using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RMAS.Models;
using RMAS.Models.BaseViewModels;
using RMAS.Models.CalendarViewModels;

namespace RMAS.Interfaces
{
    public interface IEventRepository : IDisposable
    {
        //Task<List<Event>> GetEvents(string eventName);
        //Task<List<Event>> GetEvents(DateOnly? date);
        IQueryable<BaseViewModel.Reservation> GetEvents(string? eventName, DateOnly? date);
        IQueryable<BaseViewModel.Reservation> GetEvents(int roomNumber, TimeSpan beginTime, TimeSpan endTime, List<DateOnly> dates);
        Task<List<Event>> GetEvents();
        Task<List<Event>> GetEventsFullCalendar(int roomNumber, DateOnly start, DateOnly end);
        Task AddEvents(List<Event> eventList);
        void DeleteEvents(List<Event> eventList);
        Task Save();
    }
}
