using Certificari.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;


namespace Certificari.Views
{
    /// <summary>
    /// Interaction logic for Listare.xaml
    /// </summary>
    public partial class Listare : Window
    {
        private Dictionary<string, string> _docs;

        private DataRowView[] _candidats;

        private string _destPath = null;

        private static string convertedXpsDocPath = string.Concat(System.IO.Path.GetTempPath(), "\\", "tempDoc", ".xps");

        public Listare( DataRowView[] candidats )
        {
            InitializeComponent();
            _destPath = null;
            currentDoc.Name = null;
            currentDoc.Path = null;
            _candidats = candidats;
            _docs = new Dictionary<string, string>();
            DataTable dt = new DataTable();
            try
            {
                dt = DAL.getInstance().select(DAL.baseQuerys.SDOCUMENT);
                if (dt != null) 
                {
                    _docs = dt.AsEnumerable()
                            .ToDictionary<DataRow, string, string>(row => row.Field<string>(1),
                                                                   row => row.Field<string>(2));
                    foreach(string key in _docs.Keys){
                        Console.WriteLine(key);
                        comboDocument.Items.Add(key);                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static class currentDoc
        {
            public static string Name {get;set;}
            public static string Path {get;set;}
        }

        private void comboDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                string key = ((ComboBox)sender).SelectedValue.ToString();
                Console.WriteLine(key);
                Console.WriteLine(_docs[key]);
                currentDoc.Name = key;
                currentDoc.Path = _docs[key];

                Console.WriteLine("PATH " + System.IO.Path.GetTempPath());

                if (File.Exists(convertedXpsDocPath))
                {
                    try
                    {
                        File.Delete(convertedXpsDocPath);
                    }
                    catch (Exception ex)
                    {
                        convertedXpsDocPath = string.Concat(System.IO.Path.GetTempPath(), "\\", Guid.NewGuid(), ".xps");
                    }

                }

                XpsDocument xpsDocument = DocumentPrevier.ConvertWordToXps(currentDoc.Path, convertedXpsDocPath);
                if (xpsDocument == null)
                {
                    return;
                }

                documentPreview.Document = xpsDocument.GetFixedDocumentSequence();
                documentPreview.Document.DocumentPaginator.PageSize = new Size(100, 100);
                documentPreview.FitToWidth();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured at selecting document!");

            }
        }

        private void ListareToate_Click(object sender, RoutedEventArgs e)
        {
           
                if (currentDoc.Path == null || currentDoc.Name == null)
                {
                    MessageBox.Show("No one document selected!");
                    return;
                }
                if (_destPath == null)
                {
                    MessageBox.Show("No doc destination selected!");
                    return;
                }

                string presedinte = txtPresedinte.Text;
                string membru1 = txtMembru1.Text;
                string membru2 = txtMembru2.Text;

                Report.CreateWordsDocuments(currentDoc.Path, _destPath, currentDoc.Name, _candidats, presedinte, membru1, membru2);
        
        }

        private void ListareIntrun_Click(object sender, RoutedEventArgs e)
        {
            if (currentDoc.Path == null || currentDoc.Name == null)
            {
                MessageBox.Show("No one document selected!");
                return;
            }
            if (_destPath == null)
            {
                MessageBox.Show("No doc destination selected!");
                return;
            }

            string presedinte = txtPresedinte.Text;
            string membru1 = txtMembru1.Text;
            string membru2 = txtMembru2.Text;

            Report.CreateWordsDocumentToOne(currentDoc.Path, _destPath, currentDoc.Name, _candidats, presedinte, membru1, membru2);
        }

        private void btnSaveDocPath_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(_destPath);
                _destPath = fbd.SelectedPath + "\\";
                lblDocPath.Content = _destPath;
            }

        }

    }
}
