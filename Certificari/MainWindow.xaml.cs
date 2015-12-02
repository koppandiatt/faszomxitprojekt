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
using System.Data;

namespace Certificari
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DataTable TestBinding;

        public MainWindow()
        {
            InitializeComponent();
            
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
            nomenclatorErrortblock.Visibility = Visibility.Collapsed;
            ///TODO fetch datas from DB

            TestBinding = new DataTable();
            TestBinding.Columns.Add("Name",typeof(string));
            TestBinding.Columns.Add("Path",typeof(string));
            TestBinding.Rows.Add("Alma", "ASdasdsada");
            TestBinding.Rows.Add("sadasd", "ASdasdsada");
            TestBinding.Rows.Add("kutya", "ASdasdsada");
            GridDocumentList.ItemsSource = TestBinding.DefaultView;
            

        }

        private void Add_document(object sender, RoutedEventArgs e)
        {
            nomenclatorErrortblock.Visibility = Visibility.Collapsed;
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
                TestBinding.Rows.Add(docName, docSrc);
                //TODO Update to database

            }
            catch (Exception ex)
            {
                nomenclatorErrortblock.Visibility = Visibility.Visible;
                nomenclatorErrortblock.Text = ex.Message;
            }
        }

       
    }
}
