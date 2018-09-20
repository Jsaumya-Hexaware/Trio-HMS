using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Models
{
    public class RoomBD:DbContext
    {
        public DbSet<RoomModel> Rooms { get; set; }
    }
}