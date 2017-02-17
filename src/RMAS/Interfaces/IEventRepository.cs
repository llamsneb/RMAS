using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RMAS.Models;

namespace RMAS.Interfaces
{
    public interface IEventRepository
    {
        List<Event> GetEvents(string eventName, DateTime? date);
        void AddEvents(List<Event> eventList);
    }
}
