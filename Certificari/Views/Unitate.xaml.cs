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
    /// Interaction logic for Unitate.xaml
    /// </summary>
    public partial class Unitate : Window
    {
        public Unitate()
        {
            InitializeComponent();
            DataTable dt = DAL.getInstance().select("SELECT * FROM " + DAL.Tables.Unitate);
            if(dt.Rows.Count > 0)
            {
                txtUnitate.Text = dt.Rows[0]["Denumire"].ToString();
                txtFormaOrg.Text = dt.Rows[0]["FormaOrg"].ToString();
                txtDenScurta.Text = dt.Rows[0]["DenumireScurta"].ToString();
                txtStrada.Text = dt.Rows[0]["Strada"].ToString();
                txtNr.Text = dt.Rows[0]["Nr"].ToString();
                txtBloc.Text = dt.Rows[0]["Bloc"].ToString();
                txtScara.Text = dt.Rows[0]["Scara"].ToString();
                txtAp.Text = dt.Rows[0]["Ap"].ToString();
                txtLocalitate.Text = dt.Rows[0]["Localitate"].ToString();
                txtJudet.Text = dt.Rows[0]["Judet"].ToString();
                txtCP.Text = dt.Rows[0]["cp"].ToString();
                txtTelefon.Text = dt.Rows[0]["Telefon"].ToString();
                txtFax.Text = dt.Rows[0]["Fax"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtCF.Text = dt.Rows[0]["CodFiscal"].ToString();
                txtCUI.Text = dt.Rows[0]["CUI"].ToString();
                txtBanca.Text = dt.Rows[0]["Banca"].ToString();
                txtIban.Text = dt.Rows[0]["IBAN"].ToString();
                txtNumeRep.Text = dt.Rows[0]["NumeReprezentant"].ToString();
                txtPrenumeRep.Text = dt.Rows[0]["PrenumeReprezentant"].ToString();
                txtFunctie.Text = dt.Rows[0]["Functie"].ToString();
                txtCNP.Text = dt.Rows[0]["CNP"].ToString();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string mess = "";
            if(DAL.getInstance().select("SELECT * FROM " + DAL.Tables.Unitate).Rows.Count > 0)
            {
                mess = DAL.getInstance().InsertUpdate("UPDATE " + DAL.Tables.Unitate +
                   " SET" + " Denumire=" + "'" + txtUnitate.Text + "'"
                          + ", FormaOrg=" + "'" + txtFormaOrg.Text + "'"
                          + ", DenumireScurta=" + "'" + txtDenScurta.Text + "'"
                          + ", Strada=" + "'" + txtStrada.Text + "'"
                          + ", Nr=" + "'" + txtNr.Text + "'"
                          + ", Bloc=" + "'" + txtBloc.Text + "'"
                          + ", Scara=" + "'" + txtScara.Text + "'"
                          + ", Ap=" + "'" + txtAp.Text + "'"
                          + ", Localitate=" + "'" + txtLocalitate.Text + "'"
                          + ", Judet=" + "'" + txtJudet.Text + "'"
                          + ", cp=" + "'" + txtCP.Text + "'"
                          + ", Telefon=" + "'" + txtTelefon.Text + "'"
                          + ", Fax=" + "'" + txtFax.Text + "'"
                          + ", Email=" + "'" + txtEmail.Text + "'"
                          + ", CodFiscal=" + "'" + txtCF.Text + "'"
                          + ", CUI=" + "'" + txtCUI.Text + "'"
                          + ", Banca=" + "'" + txtBanca.Text + "'"
                          + ", IBAN=" + "'" + txtIban.Text + "'"
                          + ", NumeReprezentant=" + "'" + txtNumeRep.Text + "'"
                          + ", PrenumeReprezentant=" + "'" + txtPrenumeRep.Text + "'"
                          + ", Functie=" + "'" + txtFunctie.Text + "'"
                          + ", CNP=" + "'" + txtCNP.Text + "'" + " WHERE Enabled=1"
                          );
               
            }
            else
            {
                mess = DAL.getInstance().InsertUpdate("INSERT INTO " + DAL.Tables.Unitate +
                    "(Denumire, FormaOrg, DenumireScurta, Strada, Nr, Bloc, Scara, Ap, Localitate, Judet, cp, Telefon, Fax, Email, CodFiscal, CUI, Banca, IBAN, NumeReprezentant, PrenumeReprezentant, Functie, CNP) VALUES ("
                    + "'" + txtUnitate.Text + "'"
                    + ",'" + txtFormaOrg.Text + "'"
                    + ",'" + txtDenScurta.Text + "'"
                    + ",'" + txtStrada.Text + "'"
                    + ",'" + txtNr.Text + "'"
                    + ",'" + txtBloc.Text + "'"
                    + ",'" + txtScara.Text + "'"
                    + ",'" + txtAp.Text + "'"
                    + ",'" + txtLocalitate.Text + "'"
                    + ",'" + txtJudet.Text + "'"
                    + ",'" + txtCP.Text + "'"
                    + ",'" + txtTelefon.Text + "'"
                    + ",'" + txtFax.Text + "'"
                    + ",'" + txtEmail.Text + "'"
                    + ",'" + txtCF.Text + "'"
                    + ",'" + txtCUI.Text + "'"
                    + ",'" + txtBanca.Text + "'"
                    + ",'" + txtIban.Text + "'"
                    + ",'" + txtNumeRep.Text + "'"
                    + ",'" + txtPrenumeRep.Text + "'"
                    + ",'" + txtFunctie.Text + "'"
                    + ",'" + txtCNP.Text + "')"
                    );
            }
            if (mess == "Success")
            {
                labError.Foreground = Brushes.Green;
                labError.Content = "Detaliile unitatii au fost salvate cu success";
            }
            else
            {
                labError.Foreground = Brushes.Red;
                labError.Content = "Au aparut erori la salvarea datelor!";
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
