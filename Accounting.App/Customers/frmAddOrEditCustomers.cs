using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using Accounting.DataLayer;
using System.IO;

namespace Accounting.App.Customers
{
    public partial class frmAddOrEditCustomers : Form
    {
        public frmAddOrEditCustomers()
        {
            InitializeComponent();
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string ImageName = Guid.NewGuid().ToString()+Path.GetExtension(pictureBox1.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pictureBox1.Image.Save(path + ImageName);
                Customer customers = new Customer()
                {
                   Address = txtAddress.Text,
                   Email = txtEmail.Text,
                   FullName = txtFullName.Text,
                   Mobile = txtMobile.Text,
                   Image = ImageName
                };
                using (UnitOfWork db = new UnitOfWork()) 
                {
                    db.CustomerRepository.InsertCustomer(customers);
                    db.Save();
                    DialogResult = DialogResult.OK;
                }

            }
        }
    }
}
