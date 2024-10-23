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
        UnitOfWork db = new UnitOfWork();
        public int customerID = 0;
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
                try
                {
                    if (customerID == 0)
                    {
                        db.CustomerRepository.InsertCustomer(customers);
                    }
                    else
                    {
                        customers.CustomerID = customerID; // فقط مقداردهی ID
                        bool updated = db.CustomerRepository.UpdateCustomer(customers);
                        if (!updated)
                        {
                            MessageBox.Show("Update failed.");
                        }
                    }

                    db.Save();
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void frmAddOrEditCustomers_Load(object sender, EventArgs e)
        {
            if (customerID != 0) 
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var customer = db.CustomerRepository.GetCustomerById(customerID);
                    txtEmail.Text = customer.Email;
                    txtAddress.Text = customer.Address;
                    txtFullName.Text = customer.FullName;
                    txtMobile.Text = customer.Mobile;
                    pictureBox1.ImageLocation = Application.StartupPath + "/Images/" + customer.Image;
                }

            }

            
        }
    }
}
