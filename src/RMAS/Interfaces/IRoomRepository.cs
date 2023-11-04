using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RMAS.Models;

namespace RMAS.Interfaces
{
    public interface IRoomRepository
    {
        Task <List<Room>> GetRooms();
    }
}
