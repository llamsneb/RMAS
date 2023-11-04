using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RMAS.Models;

namespace RMAS.Interfaces
{
    public interface IEventRepository : IDisposable
    {
        Task<List<Event>> GetEvents(string eventName);
        Task<List<Event>> GetEvents(DateOnly? date);
        Task<List<Event>> GetEvents(string eventName, DateOnly? date);
        Task<List<Event>> GetEvents(int roomNumber, TimeSpan beginTime, TimeSpan endTime, List<DateOnly> dates);
        Task AddEvents(List<Event> eventList);
        Task Save();
    }
}
