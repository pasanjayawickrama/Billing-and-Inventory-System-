using BillingSystem.BLL;
using BillingSystem.DAL;
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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        UserBLL u = new UserBLL();
        UserDAL dal = new UserDAL();

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            

            //Getting data from UI
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;

            u.added_date = DateTime.Now;

            //Getting username of the logged in user
            String loggeduser = frmLogin.loggedIn;
            UserBLL usr = dal.GetIdFromUsername(loggeduser);
            u.added_by = usr.id;
           

            //Inserting data into Database

            bool success = dal.Insert(u);

            //If the data is successfully inserted, then the value of success will be true else it will be false
            if (success == true)
            {
                //Data successfully inserted
                MessageBox.Show("User Successfully Created...!");
                Clear();
            }
            else
            {
                //Data insertion Failed
                MessageBox.Show("Failed to add new User...!");
            }

            //Refreshing Data Grid view

            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt; 
        }

        private void Clear()
        {
            txtUserId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";

        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get index of perticular Row
            int rowIndex = e.RowIndex;
            txtUserId.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Getting the values from Users UI
            u.id = Convert.ToInt32(txtUserId.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            //Updateing data into database

            bool success = dal.Update(u);

            //If the data is successfully Updated, then the value of success will be true else it will be false
            if (success == true)
            {
                //Data successfully inserted
                MessageBox.Show("User Update Successfull...!");
                Clear();
            }
            else
            {
                //Data insertion Failed
                MessageBox.Show("Failed Update User...!");
            }
            //Refreshing Datagrid view
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Getting user from Form

            u.id = Convert.ToInt32(txtUserId.Text);
            bool success = dal.Delete(u);

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
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get keyword from Textbox

            String keyword = txtSearch.Text;

            //Check the keyword has value or not
            if(keyword!=null)
            {
                //Show user based on the keyword
                DataTable dt = dal.Search(keyword);
                dgvUsers.DataSource = dt;
            }
            else
            {
                //Show all users from the database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

            }

        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }
    }
}
