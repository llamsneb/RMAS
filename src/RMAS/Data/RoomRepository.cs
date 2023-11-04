using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RMAS.Interfaces;

namespace RMAS.Models
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RMAS_dbContext _dbContext;
        public RoomRepository(RMAS_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _dbContext.Room.ToListAsync();
        }
    }
}
