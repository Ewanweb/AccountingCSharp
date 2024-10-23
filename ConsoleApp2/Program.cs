using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Accounting_DBEntities1 db = new Accounting_DBEntities1();
            GenericRepository<Customer> customeRepository = new GenericRepository<Customer>(db);
            var result = customeRepository.GetById(2);
            var result2 = customeRepository.Get().ToList();


            using (UnitOfWork rs = new UnitOfWork())
            {
                Customer newCustomer = new Customer()
                {
                    FullName = "John Doe",
                    Image = "john.doe@example.com",
                    Mobile = "1234567890",
                    // سایر فیلدها
                };
                customeRepository.Insert(newCustomer);
                rs.Save();
            }

            foreach (var item in result2)
            {
                Console.WriteLine($"{item.FullName} // {item.Mobile} // {item.Image}");
            }
            Console.ReadKey();
        }
    }
}
