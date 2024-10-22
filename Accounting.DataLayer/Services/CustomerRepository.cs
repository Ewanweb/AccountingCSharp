using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repositories;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private Accounting_DBEntities1 db;
        public CustomerRepository(Accounting_DBEntities1 context)
        {
            db = context;
        }
        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteCustomer(Customer Customer)
        {
            try
            {
                db.Entry(Customer).State = EntityState.Deleted;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public List<Customer> GetAllCustomers()
        {
            return db.Customers.ToList();
        }


        public bool InsertCustomer(Customer customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Added;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomersByfilter(string parameter)
        {
            return db.Customers.Where(c => c.FullName.Contains(parameter) || c.Email.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }
    }
}
