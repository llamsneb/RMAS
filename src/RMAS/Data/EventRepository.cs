﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using RMAS.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Azure.Core.HttpHeader;
using RMAS.Models.CalendarViewModels;
using RMAS.Models.BaseViewModels;
using RMAS.Models.SearchViewModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Security.Claims;

namespace RMAS.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly RMAS_dbContext _dbContext;

        public EventRepository(RMAS_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<List<Event>> GetEvents(string eventName)
        //{
        //    return await _dbContext.Event.Include(e => e.User).Where(e => e.EventName == eventName).ToListAsync();           
        //}

        //public async Task<List<Event>> GetEvents(DateOnly? date)
        //{
        //    return await _dbContext.Event.Include(e => e.User).Where(e => e.EventDate == date).ToListAsync();
        //}

        //public async Task<List<Event>> GetEvents(string eventName, DateOnly? date)
        //{
        //    return await _dbContext.Event.Include(e => e.User).Where(e => e.EventName == eventName && e.EventDate == date).ToListAsync();
        //}

        public  IQueryable<BaseViewModel.Reservation> GetEvents(string? eventName, DateOnly? date)
        {
            if (!string.IsNullOrEmpty(eventName) && date.HasValue)
            {
                return _dbContext.Event.Include(e => e.User).Where(e => e.EventName == eventName && e.EventDate == date).OrderBy(e => e.EventDate).ThenBy(e => e.BeginTime).Select(s => new SearchViewModel.Reservation { Event = s });
                
            }
            else if (!string.IsNullOrEmpty(eventName))
            {
                return  _dbContext.Event.Include(e => e.User).Where(e => e.EventName == eventName).OrderBy(e => e.EventDate).ThenBy(e => e.BeginTime).Select(s => new SearchViewModel.Reservation { Event = s });
            }
            else //if (date.HasValue)
            {
                return  _dbContext.Event.Include(e => e.User).Where(e => e.EventDate == date).OrderBy(e => e.EventDate).ThenBy(e => e.BeginTime).Select(s => new SearchViewModel.Reservation { Event = s });
            }
        }

        public async Task<List<Event>> GetEvents()
        {
            //return await _dbContext.Event.Where(e => e.RoomNumber == roomNumber).ToListAsync();
            return await _dbContext.Event.Select(e => new Event{ EventName = e.EventName, EventDate = e.EventDate, BeginTime = e.BeginTime, EndTime = e.EndTime, RoomNumber = e.RoomNumber, UserId = e.UserId }).ToListAsync();

        }

        public async Task<List<Event>> GetEventsFullCalendar(int roomNumber, DateOnly start, DateOnly end)
        {
            return await _dbContext.Event.Where(e =>
                e.RoomNumber == roomNumber
                && (e.EventDate >= start)
                && (e.EventDate <= end))
                .ToListAsync();
        }

        public IQueryable<BaseViewModel.Reservation> GetEvents(int roomNumber, TimeSpan beginTime, TimeSpan endTime, List<DateOnly> dates)
        {
            return _dbContext.Event
                .Include(e => e.User)
                .Where(e => 
                e.RoomNumber == roomNumber 
                && (e.BeginTime >= beginTime 
                && e.EndTime <= endTime) 
                && dates.Contains(e.EventDate))
                .OrderBy(e => e.EventDate)
                .ThenBy(e => e.BeginTime)
                .Select(s => new SearchViewModel.Reservation { Event = s });
        }

        public async Task AddEvents(List<Event> eventList)
        {
            await _dbContext.Event.AddRangeAsync(eventList);
        }

        public void DeleteEvents(List<Event> eventList)
        {
             _dbContext.Event.RemoveRange(eventList);
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
