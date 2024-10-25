using Accounting.DataLayer.Context;
using Accounting.ViewModels.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Buisness
{
    public class Account
    {
        public static reportViewModel ReportFormMain()
        {
            reportViewModel rp = new reportViewModel();
            using(UnitOfWork db = new UnitOfWork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,31);
                var recive = db.AccountingRepository.Get(a => a.TypeID == 1 && a.DateTime >= startDate && a.DateTime <= endDate).Select(a => a.Amount).ToList();
                var pay = db.AccountingRepository.Get(a => a.TypeID == 2 && a.DateTime >= startDate && a.DateTime <= endDate).Select(a => a.Amount).ToList();
                rp.Recive = recive.Sum();
                rp.Pay = pay.Sum();
                rp.AccountBalance = (recive.Sum() - pay.Sum());
            }
            return rp;
        }
    }
}
