using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repositories;
using Accounting.ViewModels.Customers;

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
            var local = db.Customers.Local.FirstOrDefault(f => f.CustomerID == customer.CustomerID);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges(); // ذخیره تغییرات
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // ثبت خطا
                return false;
            }
        }


        public IEnumerable<Customer> GetCustomersByfilter(string parameter)
        {
            return db.Customers.Where(c => c.FullName.Contains(parameter) || c.Email.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }

        public List<ListCustomerViewModel> GetNameCustomers(string filter = null)
        {
            if (filter == null)
            {
                return db.Customers.Select(c => new ListCustomerViewModel()
                {
                    CustomerId = c.CustomerID,
                    FullName = c.FullName,
                }).ToList();
            }

            return db.Customers.Where(c => c.FullName.Contains(filter)).Select(c => new ListCustomerViewModel()
            {
                CustomerId = c.CustomerID,
                FullName = c.FullName,
            }).ToList();
        }

        public int GetCustomerIdByName(string name)
        {
            return db.Customers.First(c => c.FullName == name).CustomerID;
        }
    }
}
