using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMAS.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMAS.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly RMAS_dbContext _dbContext;
        
        public EventRepository(RMAS_dbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task<List<Event>> GetEvents(string eventName)
        {
            return await _dbContext.Event.Include(e => e.User).Where(e => e.EventName == eventName).ToListAsync();           
        }

        public async Task<List<Event>> GetEvents(DateOnly? date)
        {
            return await _dbContext.Event.Include(e => e.User).Where(e => e.EventDate == date).ToListAsync();
        }

        public async Task<List<Event>> GetEventsFullCalendar(int roomNumber, DateOnly start, DateOnly end)
        {
            return await _dbContext.Event.Where(e =>
                e.RoomNumber == roomNumber
                && (e.EventDate >= start)
                && (e.EventDate <= end))
                .ToListAsync();
        }

        public async Task<List<Event>> GetEvents(int roomNumber, TimeSpan beginTime, TimeSpan endTime, List<DateOnly> dates)
        {
            return await _dbContext.Event.Where(e => e.RoomNumber == roomNumber && (e.BeginTime >= beginTime && e.EndTime <= endTime) && dates.Contains(e.EventDate)).ToListAsync();
        }

        public async Task AddEvents(List<Event> eventList)
        {
            await _dbContext.Event.AddRangeAsync(eventList);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
