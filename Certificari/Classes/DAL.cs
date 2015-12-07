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

        public int getInsertRowId()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SELECT SCOPE_IDENTITY()", conn);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
                //MessageBox.Show(se.Message, "DB Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1;
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
                string query = "UPDATE " + Table + " SET Status=0 WHERE Id="+Id;
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

            public const string CANDIDAT = "Candidat";
            public const string DOCUMENT = "Document";
            public const string UNITATE = "Unitate";
         
        }

        public static class baseQuerys
        {
            public const string SCANDIDAT = "SELECT * FROM Candidat WHERE Status = 1";
            public const string SDOCUMENT = "SELECT * FROM Document WHERE Status = 1";
        }

    }
}
