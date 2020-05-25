using BillingSystem.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem.DataAccessLayer
{
    class CategoriesDAL
    {
        //Static string for database connection string
        static String myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        
        public DataTable Select()
        {
            //Creating database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //Sql query to select data
                String sql = "SELECT * FROM tbl_categories";

                SqlCommand cmd = new SqlCommand(sql,conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open database connection
                conn.Open();
                //Adding the values from adapter to DataTable dt
                adapter.Fill(dt);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;


        }

        #endregion

        #region Insert new Category

        public bool Insert(CategoriesBLL c)
        {
            bool isSuccess = false;

            //Sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to add new category
                String sql = "INSERT INTO tbl_categories (title,description,added_date,added_by) VALUES(@title,@description,@added_date,@added_by)";

                SqlCommand cmd = new SqlCommand(sql,conn);

                //Passing values through parameters
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                //Open dtabase connection
                conn.Open();

                //creating a int variable to count Executing sql query
                int rows = cmd.ExecuteNonQuery();

                //Checking rows count
                if (rows > 0)
                {
                    //Query successfull
                    isSuccess = true;
                }
                else
                {
                    //Query Failed..
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

        #region Update method

        public bool Update(CategoriesBLL c)
        {
            bool isSuccess = false;

            //Creating SQL connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Sql query to update
                String sql = "UPDATE tbl_categories SET title=@title,description=@description,added_date=@added_date,added_by=@added_by WHERE id=@id";

                //Sql command to pass the value on sql query
                SqlCommand cmd = new SqlCommand(sql,conn);

                //Passing the value using command
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                conn.Open();

                //Create int variable to count executing query
                int rows = cmd.ExecuteNonQuery();

                //Checking the count whether greater than 0 or not
                if (rows > 0)
                {
                    //Query successful
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

        #region Delete method

        public bool Delete(CategoriesBLL c)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to delete from database

                String sql = "DELETE FROM tbl_categories WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql,conn);

                //Passing the values using command
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open sql connection
                conn.Open();
                //Count Query Executing
                int rows = cmd.ExecuteNonQuery();
                //If the query is executed successfully, the value of rows will be greater than 0 else less than 0

                if (rows > 0)
                {
                    //Query Success
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

        #region Search Categories
        
        public DataTable Search(String keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Creating DataTable object to hold the data temporary
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to search categories from database
                String sql = "SELECT * FROM tbl_categories WHERE id LIKE '%"+keywords+"%' OR title LIKE '%"+keywords+"%' OR description LIKE '%"+keywords+"%' ";

                //Creating SQL Command to Execute the query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Sql connection
                conn.Open();

                //Passing values from adapter to DataTable dt
                adapter.Fill(dt);
            }
            catch(Exception ex){

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion
    }
}
