using BillingSystem.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem.DAL
{
    class UserDAL
    {
        static String myconnstring = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database

        public DataTable Select()
        {
            //Static method to connect databse
            SqlConnection conn = new SqlConnection(myconnstring);
            //to hold the data from databse
            DataTable dt = new DataTable();

            try {
                //SQL query to get data from database
                String sql = "SELECT * FROM tbl_users";
                //For Executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting data from databse
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Databse connection open
                conn.Open();
                //Fill the data in our database
                adapter.Fill(dt);


            }
            catch(Exception ex) {
                //Error message
                MessageBox.Show(ex.Message);
            
            }
            finally {
                //Close database connection
                conn.Close();
            }
            //Return the value in Data table
            return dt;
        }

        #endregion

        #region Insert data in Database

        public bool Insert(UserBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                String sql = "INSERT INTO tbl_users(first_name,last_name,email,username,password,contact,address,gender,user_type,added_date,added_by) VALUES(@first_name,@last_name,@email,@username,@password,@contact,@address,@gender,@user_type,@added_date,@added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name",u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully, then the value to rows wil be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Successful
                    isSuccess = true;
                }
                else
                {
                    //Query failed
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Update data in database

        public bool Update(UserBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                String sql = "UPDATE tbl_users SET first_name=@first_name,last_name=@last_name,email=@email,username=@username,password=@password,contact=@contact,address=@address,gender=@gender,user_type=@user_type,added_date=@added_date,added_by=@added_by WHERE id=@id ";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    //Query Successful
                    isSuccess = true;
                }
                else
                {
                    //Query Failed
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Delete data in database

        public bool Delete(UserBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                String sql = "DELETE FROM tbl_users WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    //Query Successful
                    isSuccess = true;
                }
                else
                {
                    //Query failed
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Search users from database using keywords

        public DataTable Search(String keywords)
        {
            //Static method to connect databse
            SqlConnection conn = new SqlConnection(myconnstring);
            //to hold the data from databse
            DataTable dt = new DataTable();

            try
            {
                //SQL query to get data from database
                String sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keywords+"%' OR first_name LIKE '%"+keywords+"%' OR username LIKE '%"+keywords+"%' OR last_name LIKE '%"+keywords+"%'";
                //For Executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting data from databse
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Databse connection open
                conn.Open();
                //Fill the data in our database
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                //Error message
                MessageBox.Show(ex.Message);

            }
            finally
            {
                //Close database connection
                conn.Close();
            }
            //Return the value in Data table
            return dt;
        }

        #endregion

    }
}
