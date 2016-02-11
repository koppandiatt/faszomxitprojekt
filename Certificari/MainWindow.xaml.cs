using Certificari.Classes;
using Certificari.Views;
using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Certificari
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICommMainUI
    {
        protected string key = "199e162ad3dcb352c1f877bf0cd6137a";
        private DataTable _DTdocuments;
        private DataTable _DToperatii;
        private string currentOperation;
        private string currentCase;
        
        public MainWindow()
        {
            try
            {
                currentOperation = DAL.baseQuerys.SCANDIDAT;
                currentCase = "";
                InitializeComponent();
                //DataTable license = DAL.getInstance().select("Select * From LICENSE WHERE Key=" + key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        private void MenuUnitate_Click(object sender, RoutedEventArgs e)
        {
            Unitate unitate = new Unitate();
            unitate.ShowDialog();
        }

        private void CandidateAdauga_Click(object sender, RoutedEventArgs e)
        {
            switch (currentCase)
            {
                case "Candidati":
                    Candidat candidat = new Candidat(false,null,this);
                    candidat.ShowDialog();
                    break;
                case "Firme":
                    Firma firma = new Firma(false,null,this);
                    firma.ShowDialog();
                    break;
                case "Programe acreditate":
                    Programe program = new Programe(false,null,this);
                    program.ShowDialog();
                    break;
                case "Contracte":
                    Contract contract = new Contract(false, null, this);
                    contract.ShowDialog();
                    break;
                default:
                    break;
            }
            
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
                updateOperatii();
            }
            
        }


        public void updateOperatii()
        {
            try
            {
                _DToperatii = DAL.getInstance().select(currentOperation);
                if (_DToperatii != null) this.GridCandidati.ItemsSource = _DToperatii.DefaultView;
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
                    throw new Exception("Va rugam selectati calea catre document!");
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
            switch (currentCase)
            {
                case "Candidati":
                    DataRowView[] deleteRows = new DataRowView[GridCandidati.SelectedItems.Count];
                    for (int i = 0; i < GridCandidati.SelectedItems.Count; i++)
                    {

                        deleteRows[i] = (DataRowView)GridCandidati.SelectedItems[i];
                    }
                    foreach (DataRowView row in deleteRows)
                    {
                        DAL.getInstance().Delete(DAL.Tables.CANDIDAT, (int)row.Row["Id"]);
                        _DToperatii.Rows.Remove(row.Row);
                    }
                    break;
                case "Firme":
                    DataRowView[] deleteFirme = new DataRowView[GridCandidati.SelectedItems.Count];
                    for (int i = 0; i < GridCandidati.SelectedItems.Count; i++)
                    {

                        deleteFirme[i] = (DataRowView)GridCandidati.SelectedItems[i];
                    }
                    foreach (DataRowView row in deleteFirme)
                    {
                        DAL.getInstance().Delete(DAL.Tables.FIRME, (int)row.Row["Id"]);
                        _DToperatii.Rows.Remove(row.Row);
                    }
                    break;
                case "Programe acreditate":
                    DataRowView[] deleteProgram = new DataRowView[GridCandidati.SelectedItems.Count];
                    for (int i = 0; i < GridCandidati.SelectedItems.Count; i++)
                    {

                        deleteProgram[i] = (DataRowView)GridCandidati.SelectedItems[i];
                    }
                    foreach (DataRowView row in deleteProgram)
                    {
                        DAL.getInstance().Delete(DAL.Tables.PROGRAME, (int)row.Row["Id"]);
                        _DToperatii.Rows.Remove(row.Row);
                    }
                    break;
                case "Contracte":
                    DataRowView[] deleteContract = new DataRowView[GridCandidati.SelectedItems.Count];
                    for (int i = 0; i < GridCandidati.SelectedItems.Count; i++)
                    {

                        deleteContract[i] = (DataRowView)GridCandidati.SelectedItems[i];
                    }
                    foreach (DataRowView row in deleteContract)
                    {
                        DAL.getInstance().Delete(DAL.Tables.CONTRACT, (int)row.Row["Id"]);
                        _DToperatii.Rows.Remove(row.Row);
                    }
                    break;
                default:
                    break;
            }

            
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Word documents(*.doc;*.docx)|*.doc;*.docx"; 
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

            switch (currentCase)
            {
                case "Candidati":
                    Candidat candidat = new Candidat(true,row);
                    candidat.ShowDialog();
                    break;
                case "Firme":
                    Firma firma = new Firma(true, row);
                    firma.ShowDialog();
                    break;
                case "Programe acreditate":
                    Programe program = new Programe(true, row);
                    program.ShowDialog();
                    break;
                case "Contracte":
                    Contract contract = new Contract(true, row);
                    contract.ShowDialog();
                    break;
                default:
                    break;
            }
            

        }


        private void Editare_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = getSelectedRow();

            switch (currentCase)
            {
                case "Candidati":
                    Candidat candidat = new Candidat(false, row,this);
                    candidat.ShowDialog();
                    break;
                case "Firme":
                    Firma firma = new Firma(false, row, this);
                    firma.ShowDialog();
                    break;
                case "Programe acreditate":
                    Programe program = new Programe(false, row, this);
                    program.ShowDialog();
                    break;
                case "Contracte":
                    Contract contract = new Contract(false, row, this);
                    contract.ShowDialog();
                    break;
                default:
                    break;
            }
            
        }

        private void btnListare_Click(object sender, RoutedEventArgs e)
        {
            if (GridCandidati.SelectedItems.Count == 0)
            {
                MessageBox.Show("Va rugam selectati minim 1 candidat!");
                return;
            }
             DataRowView[] candidatsRows = new DataRowView[GridCandidati.SelectedItems.Count];
             for (int i = 0; i < GridCandidati.SelectedItems.Count; i++)
             {

                 candidatsRows[i] = (DataRowView)GridCandidati.SelectedItems[i];
             }
            Listare listare = new Listare(candidatsRows);
            listare.ShowDialog();
        }

        private DataRow getSelectedRow()
        {
            return ((DataRowView)GridCandidati.SelectedItem).Row;
        }
       
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            
            
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyWord = txtSearch.Text.ToLower().TrimStart().TrimEnd();
            var filtered = _DToperatii.AsEnumerable().Where(
                    r => r.Field<String>("NrMatricol").ToLower().Contains(keyWord)
                    || r.Field<String>("Nume").ToLower().Contains(keyWord)
                    || r.Field<String>("Prenume").ToLower().Contains(keyWord)
                    || r.Field<String>("Localitate").ToLower().Contains(keyWord)
                    || r.Field<String>("CNP").ToLower().Contains(keyWord)
                );
            switch (currentCase)
            {
                case "Candidati":
                    filtered = _DToperatii.AsEnumerable().Where(
                        r => r.Field<String>("NrMatricol").ToLower().Contains(keyWord)
                        || r.Field<String>("Nume").ToLower().Contains(keyWord)
                        || r.Field<String>("Prenume").ToLower().Contains(keyWord)
                        || r.Field<String>("Localitate").ToLower().Contains(keyWord)
                        || r.Field<String>("CNP").ToLower().Contains(keyWord)
                    );
                    break;
                case "Firme":
                    filtered = _DToperatii.AsEnumerable().Where(
                        r => r.Field<String>("Denumire").ToLower().Contains(keyWord)
                        || r.Field<String>("Localitate").ToLower().Contains(keyWord)
                        || r.Field<String>("Telefon").ToLower().Contains(keyWord)
                        || r.Field<String>("Email").ToLower().Contains(keyWord)
                        || r.Field<String>("ORC").ToLower().Contains(keyWord)
                    );
                    break;
                case "Programe acreditate":
                    filtered = _DToperatii.AsEnumerable().Where(
                        r => r.Field<String>("Denumire").ToLower().Contains(keyWord)
                        || r.Field<String>("SerieA").ToLower().Contains(keyWord)
                        || r.Field<String>("Calificare").ToLower().Contains(keyWord)
                        || r.Field<String>("Domeniu").ToLower().Contains(keyWord)
                        || r.Field<String>("COR").ToLower().Contains(keyWord)
                    );
                    break;
                case "Contracte":
                    filtered = _DToperatii.AsEnumerable().Where(
                        r => r.Field<String>("NrContract").ToLower().Contains(keyWord)
                        || r.Field<String>("Beneficiar").ToLower().Contains(keyWord)
                        || r.Field<String>("ProgramContract").ToLower().Contains(keyWord)
                    );
                    break;
                default:
                    break;
            }
            
            if (filtered.Count() > 0)
            {
                DataTable dt = new DataTable();
                dt = filtered.CopyToDataTable();
                GridCandidati.ItemsSource = dt.DefaultView;
            }
            else
            {
                GridCandidati.ItemsSource = null;
            }
        }

        private void rb_candidat_Checked(object sender, RoutedEventArgs e)
        {
            currentCase = rb_candidat.Content.ToString();
            currentOperation = DAL.baseQuerys.SCANDIDAT;
            updateOperatii();
            Console.WriteLine(currentCase);
        }

        private void rb_firme_Checked(object sender, RoutedEventArgs e)
        {
            currentCase = rb_firme.Content.ToString();
            currentOperation = DAL.baseQuerys.SFIRME;
            updateOperatii();
            Console.WriteLine(currentCase);
        }

        private void rb_programe_Checked(object sender, RoutedEventArgs e)
        {
            currentCase = rb_programe.Content.ToString();
            currentOperation = DAL.baseQuerys.SPROGRAME;
            updateOperatii();
            Console.WriteLine(currentCase);
        }

        private void rb_contracte_Checked(object sender, RoutedEventArgs e)
        {
            currentCase = rb_contracte.Content.ToString();
            currentOperation = DAL.baseQuerys.SCONTRACT;
            updateOperatii();
            Console.WriteLine(currentCase);
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            Despre d = new Despre();
            d.ShowDialog();
        }

        private void MenuIesire_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
