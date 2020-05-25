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
using System.Xml.Serialization;

namespace BillingSystem.UI
{
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        CategoriesBLL c = new CategoriesBLL();
        CategoriesDAL dal = new CategoriesDAL();
        UserDAL udal = new UserDAL();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the values from categories form
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            //Getting id in addedBy field
            String loggedUser = frmLogin.loggedIn;
            UserBLL usr = udal.GetIdFromUsername(loggedUser);

            //Passing the id logged in user to added by field
            c.added_by = usr.id;

            //Creating boolian method to insert data into database
            bool succes = dal.Insert(c);

            if(succes == true)
            {
                //Successfully inserted
                MessageBox.Show("Successfully inserted new Category...!");
                //Refresh datagrid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
                //clear insertions from text boxes
                Clear();
            }
            else
            {
                //Insertion failed
                MessageBox.Show("Insertion failed...!");
            }

        }

        public void Clear()
        {
            txtCategoryId.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            //Display all the categories in database
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowIndex = e.RowIndex;
            txtCategoryId.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from categories form
            c.id = int.Parse(txtCategoryId.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            //Getting id in addedBy field
            String loggedUser = frmLogin.loggedIn;
            UserBLL usr = udal.GetIdFromUsername(loggedUser);

            //Passing the id logged in user to added by field
            c.added_by = usr.id;

            //Creating boolian cariable to update categories
            bool success = dal.Update(c);
            if (success == true)
            {
                //Successfully updated.
                MessageBox.Show("Successfully update the category");
                Clear();
                //Refresh Datagrid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;

            }
            else
            {
                MessageBox.Show("Category updating Failed...!");
                Clear();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the id of the category which we want to delete
            c.id = int.Parse(txtCategoryId.Text);

            //Creating boolian variable to delete the category
            bool success = dal.Delete(c);

            //If the category deleted successfully, then the success value will be true, so checking it
            if (success == true)
            {
                MessageBox.Show("Category Delted Successfully...!");
                Clear();
                //Refresh Datagrid view
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Category deletion Failed...!");
                Clear();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keywords
            String keywords = txtSearch.Text;

            //Filter the categories based on keywords
            if (keywords != null)
            {
                //Use search method to display caegories
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Just use select method to display categories. Not using search
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }
    }
}
