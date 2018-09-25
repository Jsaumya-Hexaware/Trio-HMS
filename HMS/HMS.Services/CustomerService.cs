using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using ProjectClassLibrary;

namespace HMS.Services
{
    public class CustomerService : ICustomer
    {
        hmsprojectEntities obj = new hmsprojectEntities();

        public bool ChangePas(Customer cust, int id)
        {
            Customer customer = obj.Customers.Where(a => a.CustomerId.Equals(id)).FirstOrDefault();
            if (customer == null)
            {
                return false;
            }
            else {
                customer.Password = cust.Password;
                obj.Entry(customer).State = EntityState.Modified;
                obj.SaveChanges();
                return true;
            }

        }

        public Booking Overlaps(Booking model)
        { 

            var Rid = obj.Rooms.Where(r => r.RoomStatus == 0 && r.Description.Equals(model.Description) && r.RoomType.Equals(model.RoomType)).FirstOrDefault();
            var ran = obj.Rooms.Where(r => r.Description.Equals(model.Description) && r.RoomType.Equals(model.RoomType)).FirstOrDefault();
            var Bid = new Booking();
            if (Rid == null)
            {
                Bid = obj.Bookings.Where(b => b.RoomId.Equals(ran.RoomId) && (b.checkIn <= model.checkOut) && (model.checkIn < b.checkOut)).FirstOrDefault();
                return Bid;

            }
            Bid.RoomId = Rid.RoomId;
            return Bid;
        }

        public Customer Validate(Customer model)
        {
            var c = obj.Customers.Where(a => a.Email.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();
            return c;

        }


    }
}