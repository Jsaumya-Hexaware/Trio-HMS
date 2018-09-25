using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectClassLibrary;

namespace HMS.Services
{
   public interface ICustomer
    {
        Customer Validate(Customer model);
        Booking Overlaps(Booking model);
        bool ChangePas(Customer cust, int id);

    }
}
