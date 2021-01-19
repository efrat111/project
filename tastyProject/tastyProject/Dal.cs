using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tastyProject
{
    public static class Dal
    {
        // public static SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\DataBase\\Tasty.mdf;Integrated Security=True;User Instance=True");
        public static SqlConnection con = new SqlConnection("Data Source=MALUL;Initial Catalog=Tasty;Integrated Security=True");
        static SqlDataAdapter adapter;

        public static void ExecuteNonQuery(string sp, List<SqlParameter> param)
        {
            SqlCommand com = new SqlCommand(sp, con);
            com.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter p in param)
            {
                com.Parameters.Add(p);
            }

            try
            {
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public static DataTable getTable(string sp, List<SqlParameter> param)
        {
            SqlCommand com = new SqlCommand(sp, con);
            DataTable dt = new DataTable();

            if (param != null)
            {
                foreach (SqlParameter p in param)
                    com.Parameters.Add(p);
            }

            com.CommandType = CommandType.StoredProcedure;

            try
            {
                adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                dt = null;
            }

            return dt;
        }

        public static object Scalar(string sp, List<SqlParameter> param)
        {
            object outPut = null;
            SqlCommand com = new SqlCommand(sp, con);
            com.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter p in param)
            {
                com.Parameters.Add(p);
            }

            try
            {
                con.Open();
                outPut =com.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return outPut;
        }

        public static bool ValidationCon()
        {
            bool conGood = true;
            try
            {
                con.Open();
            }
            catch
            {
                conGood = false;
            }
            finally
            {
                con.Close();
            }
            return conGood;
        }
    }
}