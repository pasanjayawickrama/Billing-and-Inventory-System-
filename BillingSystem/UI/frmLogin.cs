using BillingSystem.BusinessLogicLayer;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

        public static String loggedIn;

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.userType = cmbUserType.Text.Trim();

            //Checking the loging credentials
            bool success = dal.loginCheck(l);

            if (success == true)
            {
                //Login Successful
                loggedIn = l.username;

                //Open respective forms based on user tye Admin or User
                switch (l.userType)
                {
                    case "Admin":
                        {
                            //Display Admin Dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();

                        }
                        break;
                    case "User":
                        {
                            //Display User Dashboard
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();

                        }
                        break;
                    default:
                        {
                            MessageBox.Show("Invalid User type...!");
                        }
                        break;
                }

            }
            else
            {
                //Login Failed
                MessageBox.Show("Login Failed...!");
            }


        }
    }
}
