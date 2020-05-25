using BillingSystem.BLL;
using BillingSystem.BusinessLogicLayer;
using BillingSystem.DAL;
using BillingSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem.UI
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //
        }

        CategoriesDAL cdal = new CategoriesDAL();
        ProductsBLL p = new ProductsBLL();
        productsDAL pdal = new productsDAL();
        UserDAL udal = new UserDAL();


        private void frmProducts_Load(object sender, EventArgs e)
        {
            //Creating datatable to hold the category data from database
            DataTable categoriesDT = cdal.Select();

            //Specify Datasource for category ComboBox
            cmbCategory.DataSource = categoriesDT;

            //Specify Display member and Value Member for ComboBox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";
            //Load data to datagrid view from database
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;

            //Getting username of loggedIn user
            String loggedUsr = frmLogin.loggedIn;
            UserBLL usr = udal.GetIdFromUsername(loggedUsr);

            p.added_by = usr.id;

            //Create variable to check if the Product added successfully or not
            bool success = pdal.Insert(p);

            //If the product is added successfully then the value of success will be true else it will be false
            if (success == true)
            {
                MessageBox.Show("Product Added successfully...!");
                //Clear the text Boxes
                Clear();
                //Refreshing the DatagridView
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Add new product...!");
                Clear();
            }
        }

        public void Clear()
        {
            txtProductId.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Create Integer variable to know which product is clicked
            int rowIndex = e.RowIndex;

            //Display the values of respective Text Boxes
            txtProductId.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from UI product form
            p.id = int.Parse(txtProductId.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;
            //Getting username of loggedIn user
            String loggedUsr = frmLogin.loggedIn;
            UserBLL usr = udal.GetIdFromUsername(loggedUsr);

            p.added_by = usr.id;

            //Create boolian variable for check if the product is update or not
            bool success = pdal.Update(p);

            if (success == true)
            {
                MessageBox.Show("Product Updated Successfully...!");
                //Clear the text Boxes
                Clear();
                //Refreshing the DatagridView
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Update the Product...!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Getting user from Form

            p.id = Convert.ToInt32(txtProductId.Text);

            bool success = pdal.Delete(p);

            //If the data is Deleted successfully , then the value of success will be true. Else it will be false
            if (success == true)
            {
                MessageBox.Show("User Successfully Deleted...!");
            }
            else
            {
                MessageBox.Show("Cannot delete the User...!");
            }
            //Refreshing Datagrid View
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }
    }
}
