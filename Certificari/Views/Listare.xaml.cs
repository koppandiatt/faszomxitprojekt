using Certificari.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Certificari.Views
{
    /// <summary>
    /// Interaction logic for Listare.xaml
    /// </summary>
    public partial class Listare : Window
    {
        Dictionary<string, string> Docs;
        public Listare()
        {
            InitializeComponent();
            Docs = new Dictionary<string, string>();
            DataTable dt = new DataTable();
            try
            {
                dt = DAL.getInstance().select(DAL.baseQuerys.SDOCUMENT);
                if (dt != null) 
                {
                    Docs = dt.AsEnumerable()
                            .ToDictionary<DataRow, string, string>(row => row.Field<string>(1),
                                                                   row => row.Field<string>(2));
                    foreach(string key in Docs.Keys){
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
            string key = ((ComboBox)sender).SelectedValue.ToString();
            Console.WriteLine(key);
            Console.WriteLine(Docs[key]);
            currentDoc.Name = key;
            currentDoc.Path = Docs[key];
        }
    }
}
