using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RMAS.Interfaces;

namespace RMAS.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly RMAS_dbContext _dbContext;
        public EventRepository(RMAS_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Event> GetEvents(string eventName = null, DateTime ? date = null)
        {
            List<Event> searchResults;
            if (date == null || date == DateTime.MinValue)
            {
                searchResults = _dbContext.Event.Where(e => e.EventName == eventName).ToList();
            }
            else if (eventName == null || eventName == "")
            {
                searchResults = _dbContext.Event.Where(e => e.EventDate == date).ToList();
            }
            else
            {
                searchResults = _dbContext.Event.Where(e => e.EventName == eventName && e.EventDate == date).ToList();
            }

            return searchResults;
        }

        public void AddEvents(List<Event> eventList)
        {
            _dbContext.Event.AddRange(eventList);
            _dbContext.SaveChanges();
        }        
    }
}
