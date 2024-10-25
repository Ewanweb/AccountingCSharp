using Accounting.Utility.Convertor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                lblDate.Text = DateConvertor.ToShamsi(DateTime.Now);
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
                this.Show();
            }
            else
            {
                RtlMessageBox.Show("کاربری یافت نشد","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
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
    }
}
