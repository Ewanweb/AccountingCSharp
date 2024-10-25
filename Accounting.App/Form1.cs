using Accounting.Utility.Convertor;
using Accounting.Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.ViewModels.Accounting;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Hide();
            frmLogin frm = new frmLogin();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateConvertor.ToShamsi(DateTime.Now);
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
                Report();
            }
            else
            {
                RtlMessageBox.Show("کاربری یافت نشد","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        void Report()
        {
            reportViewModel report = Account.ReportFormMain();
            lblPay.Text = report.Pay.ToString("#,0, تومان ");
            lblRecive.Text = report.Recive.ToString("#,0, تومان ");
            lblAccountbalance.Text = report.AccountBalance.ToString("#,0, تومان ");
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.ShowDialog(); 
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
            frmNewAccounting frm = new frmNewAccounting();
            frm.ShowDialog();
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.TypeId = 2;
            frm.ShowDialog();
        }

        private void btnReportRecive_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.TypeId = 1;
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnLoginSetting_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.IsEdit = true; 
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
