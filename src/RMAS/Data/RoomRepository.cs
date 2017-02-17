using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<int> GetRoomNumbers(string roomType)
        {
            return _dbContext.Room.Where(r => r.RoomType == roomType).Select(r => r.RoomNumber).ToList();
        }
    }
}
