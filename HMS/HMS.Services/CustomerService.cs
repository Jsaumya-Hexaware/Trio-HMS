using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectClassLibrary;

namespace HMS.Services
{
    public class CustomerService : ICustomer
    {
        hmsprojectEntities obj = new hmsprojectEntities();
        public Customer Validate(Customer model)
        {
          var c=  obj.Customers.Where(a => a.Email.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();
            return c;
            
        }
    }
}
