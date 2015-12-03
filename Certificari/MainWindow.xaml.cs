using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Certificari.Views;
using Certificari.Classes;
using System.Data;
using System.Windows.Controls.Primitives;

namespace Certificari
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DataTable _DTdocuments;
        private DataTable _DTcandidats;

        public MainWindow()
        {
            InitializeComponent();
            /*string mess = DAL.getInstance().iud("INSERT INTO Document (Nume,Path) VALUES ('Foaie Matricola', 'D:/Documente/Scoala/')");
            MessageBox.Show(mess);
            DataTable dt = DAL.getInstance().select("SELECT * FROM Document");
            MessageBox.Show(dt.Rows.Count.ToString());*/
           
            
        }

        private void MenuUnitate_Click(object sender, RoutedEventArgs e)
        {
            Unitate unitate = new Unitate();
            unitate.ShowDialog();
            int a = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Candidat candidat = new Candidat();
            candidat.ShowDialog();
       
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != sender)
            {
                return;
            }
          
            if (sender is TabControl && tabNomenclator.IsSelected)
            {
                Console.WriteLine("sadsad");
            nomenclatorErrortblock.Text = "";
            ///TODO fetch datas from DB

            _DTdocuments = new DataTable();
            _DTdocuments.Columns.Add("Name", typeof(string));
            _DTdocuments.Columns.Add("Path", typeof(string));
            _DTdocuments.Rows.Add("Alma", "ASdasdsada");
            _DTdocuments.Rows.Add("sadasd", "ASdasdsada");
            _DTdocuments.Rows.Add("kutya", "ASdasdsada");
            _DTdocuments.Rows.Add("sadasd", "ASdasdsada");
            _DTdocuments.Rows.Add("kutya", "ASdasdsada");
            _DTdocuments.Rows.Add("sadasd", "ASdasdsada");
            _DTdocuments.Rows.Add("sadasd", "ASdasdsada");
            _DTdocuments.Rows.Add("kutya", "ASdasdsada");
            _DTdocuments.Rows.Add("sadasd", "ASdasdsada");
            _DTdocuments.Rows.Add("kutya", "ASdasdsada");

            _DTdocuments.Rows.Add("kutya", "ASdasdsada");
            GridDocumentList.ItemsSource = _DTdocuments.DefaultView;
            

           }

            if (sender is TabControl && tabOperati.IsSelected)
            {
               _DTcandidats = DAL.getInstance().select("Select * FROM Candidat");
               this.GridCandidati.ItemsSource = _DTcandidats.DefaultView;
            }

        }

        private void Add_document(object sender, RoutedEventArgs e)
        {
            nomenclatorErrortblock.Text = "";
            string docSrc = docSrctxtbox.Text;
            string docName = docNametxtbox.Text;

            try
            {
                if (docSrc == String.Empty || docSrc.Trim() == "")
                {
                    throw new Exception("Empty document source path!");
                }
                if (docName == String.Empty || docName.Trim() == "")
                {
                    throw new Exception("Empty document Name!");
                }
                _DTdocuments.Rows.Add(docName, docSrc);
                //TODO Update to database

            }
            catch (Exception ex)
            {
                nomenclatorErrortblock.Text = ex.Message;
            }
        }

    
       

        private void Sterge_Click(object sender, RoutedEventArgs e)
        {



                DataRowView[] deleteRows = new DataRowView[GridDocumentList.SelectedItems.Count];
                for (int i = 0; i < GridDocumentList.SelectedItems.Count; i++)
                {

                    deleteRows[i] = (DataRowView)GridDocumentList.SelectedItems[i];
                }
                foreach (DataRowView row in deleteRows)
                {
                    _DTdocuments.Rows.Remove(row.Row);
                }
            

        }

        private void stergeStudents_Click(object sender, RoutedEventArgs e)
        {


            DataRowView[] deleteRows = new DataRowView[GridCandidati.SelectedItems.Count];
            for (int i = 0; i < GridCandidati.SelectedItems.Count; i++)
            {
                deleteRows[i] = (DataRowView)GridCandidati.SelectedItems[i];
            }
            foreach (DataRowView row in deleteRows)
            {
                _DTcandidats.Rows.Remove(row.Row);
            }
        }

       

       
    }
}
