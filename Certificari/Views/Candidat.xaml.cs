using Certificari.Classes;
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
using System.Windows.Shapes;

namespace Certificari.Views
{
    /// <summary>
    /// Interaction logic for Candidat.xaml
    /// </summary>
    public partial class Candidat : Window
    {
        public Candidat()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO Candidat (" +
                "NrMatricol, Nume, Prenume, Tata, Mama, CNP, DataNasterii, LoculNasterii, Strada, Nr, Bloc," +
                "Scara, Ap, Localitate, Judet, Cp, Telefon, Studii, Profesia, LocMunca) VALUES ('" +
                txtNumar.Text + "','" +
                txtNume.Text + "','" +
                txtPrenume.Text + "','" +
                txtTata.Text + "','" +
                txtMama.Text + "','" +
                txtCNP.Text + "','" +
                txtDataNasterii.Text + "','" +
                txtLoculN.Text + "','" +
                txtStrada.Text + "','" +
                txtNr.Text + "','" +
                txtBloc.Text + "','" +
                txtScara.Text + "','" +
                txtAp.Text + "','" +
                txtLocalitatea.Text + "','" +
                txtJudet.Text + "','" +
                txtCP.Text + "','" +
                txtTelefon.Text + "','" +
                txtStudii.Text + "','" +
                txtProfesia.Text + "','" +
                txtLocMunca.Text + "')";

            if (DAL.getInstance().iud(query) == "Success")
            {
                this.Close();
            }
            else
            {
                labelError.Content = "Erroare la inserarea datelor in baza de date";
            }
            
        }
    }
}
