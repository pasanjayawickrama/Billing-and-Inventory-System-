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
    class productsDAL
    {
        //String method for DB connection
        static String myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method for Products

        public DataTable Select()
        {
            //Creating database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //Sql query to select data
                String sql = "SELECT * FROM tbl_products";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open database connection
                conn.Open();
                //Adding the values from adapter to DataTable dt
                adapter.Fill(dt);

            }
            catch (Exception ex)
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

        #region Insert method

        public bool Insert(ProductsBLL p)
        {
            bool isSuccess = false;

            //Sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to add new category
                String sql = "INSERT INTO tbl_products (name,category,description,rate,qty,added_date,added_by) VALUES(@name,@category,@description,@rate,@qty,@added_date,@added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

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
            catch (Exception ex)
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

        #region Update Products

        public bool Update(ProductsBLL p)
        {
            bool isSuccess = false;

            //Creating SQL connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Sql query to update
                String sql = "UPDATE tbl_products SET name=@name,category=@category,description=@description,rate=@rate,added_date=@added_date,added_by=@added_by WHERE id=@id";

                //Sql command to pass the value on sql query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing values through parameters
                cmd.Parameters.AddWithValue("@id", p.id);
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

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
            catch (Exception ex)
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

        #region Delete products

        public bool Delete(ProductsBLL p)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to delete from database

                String sql = "DELETE FROM tbl_products WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the values using command
                cmd.Parameters.AddWithValue("@id", p.id);

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
            catch (Exception ex)
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
    }
}
