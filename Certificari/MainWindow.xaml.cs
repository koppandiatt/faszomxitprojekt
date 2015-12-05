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
using Certificari.Views;
using Certificari.Classes;
using System.Data;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;

namespace Certificari
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICommMainUI
    {

        private DataTable _DTdocuments;
        private DataTable _DTcandidats;
     

        public MainWindow()
        {
            InitializeComponent();
        
        }

        private void MenuUnitate_Click(object sender, RoutedEventArgs e)
        {
            Unitate unitate = new Unitate();
            unitate.ShowDialog();
        }

        private void CandidateAdauga_Click(object sender, RoutedEventArgs e)
        {
            Candidat candidat = new Candidat(false,null,this);
            candidat.ShowDialog();
           // RefreshCandidati();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != sender)
            {
                return;
            }
          
            if (sender is TabControl && tabNomenclator.IsSelected)
            {           
                nomenclatorErrortblock.Content = "";
                updateDocumentList();

           }

            if (sender is TabControl && tabOperati.IsSelected)
            {
                updateCandidats();
            }

        }


        public void updateCandidats()
        {
            try
            {
                _DTcandidats = DAL.getInstance().select(DAL.baseQuerys.SCANDIDAT);
                if (_DTcandidats != null) this.GridCandidati.ItemsSource = _DTcandidats.DefaultView;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public void updateDocumentList()
        {
            _DTdocuments = DAL.getInstance().select(DAL.baseQuerys.SDOCUMENT);
            if (_DTdocuments != null) this.GridDocumentList.ItemsSource = _DTdocuments.DefaultView;
        }

        private void Add_document(object sender, RoutedEventArgs e)
        {
            nomenclatorErrortblock.Content = "";
            string docSrc = docSrctxtbox.Content.ToString();
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
              
              
                DAL.getInstance().InsertUpdate("INSERT Document(Nume,Path) VALUES( '" +
                                                docName + "' , '" + docSrc + "' ) ");

                

              
                Console.WriteLine(" IDDDDDDDDDDDD " + DAL.getInstance().getInsertRowId());

                updateDocumentList();



            }
            catch (Exception ex)
            {
                nomenclatorErrortblock.Content = ex.Message;
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
                    DAL.getInstance().Delete(DAL.Tables.DOCUMENT,(int) row.Row["Id"]);
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
                DAL.getInstance().Delete(DAL.Tables.CANDIDAT, (int)row.Row["Id"]);
                _DTcandidats.Rows.Remove(row.Row);
            }
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                this.docSrctxtbox.Content = Path.GetFullPath(ofd.FileName);
            }
        }

       
          

        private void GridCandidati_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (GridCandidati.SelectedItems.Count == 1) menuCandi.Visibility = Visibility.Visible;
            else menuCandi.Visibility = Visibility.Collapsed;
        }

        private void Detalii_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = getSelectedRow();
            Candidat candidat = new Candidat(true,row);
            candidat.ShowDialog();

        }


        private void Editare_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = getSelectedRow();
            Candidat candidat = new Candidat(false, row,this);
            candidat.ShowDialog();
        }

        private DataRow getSelectedRow()
        {
            return ((DataRowView)GridCandidati.SelectedItem).Row;
        }





      
    }
}
