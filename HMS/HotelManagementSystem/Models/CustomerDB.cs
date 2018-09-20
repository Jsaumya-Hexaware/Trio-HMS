using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Models
{
    public class CustomerDB:DbContext
    {
        public DbSet<CustomerModel> Customers { get; set; }
    }
}