using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Certificari.Classes
{
    public class DAL
    {
        private static DAL instance = null;
        private SqlConnection conn;

        private DAL(){
            try
            {
                conn = new SqlConnection(Certificari.Properties.Settings.Default.ConnectionString);
                conn.Open();
            }
            catch (SqlException se)
            {
                //MessageBox.Show(se.Message, "DB Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(se.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                //MessageBox.Show(e.Message, "DB Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public static DAL getInstance()
        {
            if(instance==null){
                instance = new DAL();
            }
            return instance;
        }

        public SqlConnection getConnection()
        {
            return this.conn;
        }

        public DataTable select(String query)
        {
            try
            {

                SqlCommand command = new SqlCommand(query, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                return dt;

            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
                //MessageBox.Show(se.Message, "DB Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
          
        }

        public string InsertUpdate(String query)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch(SqlException se)
            {
                Console.WriteLine(query);
                Console.WriteLine(se.Message);
                return "Insert Error!";
            }

            return "Success";
        }

        public string Delete(string Table, int Id)
        {
            try
            {
                string query = "UPDATE " + Table + "SET Status=0 WHERE Id="+Id;
                using(SqlCommand com = new SqlCommand(query, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            catch(SqlException se){
                Console.WriteLine(se.Message);
                return "Error!";
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return "Error!";
            }

            return "Success";
        }

        public static class Tables
        {

            public const string Candidat = "Candidat";
            public const string Document = "Document";
            public const string Unitate = "Unitate";
         
        }

    }
}
