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
            DataTable dt = DAL.getInstance().select("SELECT * FROM " + DAL.Tables.UNITATE);
            if(dt.Rows.Count > 0)
            {
                txtUnitate.Text = dt.Rows[0]["Denumire"].ToString().Trim();
                txtFormaOrg.Text = dt.Rows[0]["FormaOrg"].ToString().Trim();
                txtDenScurta.Text = dt.Rows[0]["DenumireScurta"].ToString().Trim();
                txtStrada.Text = dt.Rows[0]["Strada"].ToString().Trim();
                txtNr.Text = dt.Rows[0]["Nr"].ToString().Trim();
                txtBloc.Text = dt.Rows[0]["Bloc"].ToString().Trim();
                txtScara.Text = dt.Rows[0]["Scara"].ToString().Trim();
                txtAp.Text = dt.Rows[0]["Ap"].ToString().Trim();
                txtLocalitate.Text = dt.Rows[0]["Localitate"].ToString().Trim();
                txtJudet.Text = dt.Rows[0]["Judet"].ToString().Trim();
                txtCP.Text = dt.Rows[0]["cp"].ToString().Trim();
                txtTelefon.Text = dt.Rows[0]["Telefon"].ToString().Trim();
                txtFax.Text = dt.Rows[0]["Fax"].ToString().Trim();
                txtEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                txtCF.Text = dt.Rows[0]["CodFiscal"].ToString().Trim();
                txtCUI.Text = dt.Rows[0]["CUI"].ToString().Trim();
                txtBanca.Text = dt.Rows[0]["Banca"].ToString().Trim();
                txtIban.Text = dt.Rows[0]["IBAN"].ToString().Trim();
                txtNumeRep.Text = dt.Rows[0]["NumeReprezentant"].ToString().Trim();
                txtPrenumeRep.Text = dt.Rows[0]["PrenumeReprezentant"].ToString().Trim();
                txtFunctie.Text = dt.Rows[0]["Functie"].ToString().Trim();
                txtCNP.Text = dt.Rows[0]["CNP"].ToString().Trim();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string mess = "";
            if(DAL.getInstance().select("SELECT * FROM " + DAL.Tables.UNITATE).Rows.Count > 0)
            {
                mess = DAL.getInstance().InsertUpdate("UPDATE " + DAL.Tables.Unitate +
                   " SET" + " Denumire=" + "'" + txtUnitate.Text.Trim() + "'"
                          + ", FormaOrg=" + "'" + txtFormaOrg.Text.Trim() + "'"
                          + ", DenumireScurta=" + "'" + txtDenScurta.Text.Trim() + "'"
                          + ", Strada=" + "'" + txtStrada.Text.Trim() + "'"
                          + ", Nr=" + "'" + txtNr.Text.Trim() + "'"
                          + ", Bloc=" + "'" + txtBloc.Text.Trim() + "'"
                          + ", Scara=" + "'" + txtScara.Text.Trim() + "'"
                          + ", Ap=" + "'" + txtAp.Text.Trim() + "'"
                          + ", Localitate=" + "'" + txtLocalitate.Text.Trim() + "'"
                          + ", Judet=" + "'" + txtJudet.Text.Trim() + "'"
                          + ", cp=" + "'" + txtCP.Text.Trim() + "'"
                          + ", Telefon=" + "'" + txtTelefon.Text.Trim() + "'"
                          + ", Fax=" + "'" + txtFax.Text.Trim() + "'"
                          + ", Email=" + "'" + txtEmail.Text.Trim() + "'"
                          + ", CodFiscal=" + "'" + txtCF.Text.Trim() + "'"
                          + ", CUI=" + "'" + txtCUI.Text.Trim() + "'"
                          + ", Banca=" + "'" + txtBanca.Text.Trim() + "'"
                          + ", IBAN=" + "'" + txtIban.Text.Trim() + "'"
                          + ", NumeReprezentant=" + "'" + txtNumeRep.Text.Trim() + "'"
                          + ", PrenumeReprezentant=" + "'" + txtPrenumeRep.Text.Trim() + "'"
                          + ", Functie=" + "'" + txtFunctie.Text.Trim() + "'"
                          + ", CNP=" + "'" + txtCNP.Text.Trim() + "'" + " WHERE Enabled=1"
                          );
               
            }
            else
            {
                mess = DAL.getInstance().InsertUpdate("INSERT INTO " + DAL.Tables.UNITATE +
                    "(Denumire, FormaOrg, DenumireScurta, Strada, Nr, Bloc, Scara, Ap, Localitate, Judet, cp, Telefon, Fax, Email, CodFiscal, CUI, Banca, IBAN, NumeReprezentant, PrenumeReprezentant, Functie, CNP) VALUES ("
                    + "'" + txtUnitate.Text.Trim() + "'"
                    + ",'" + txtFormaOrg.Text.Trim() + "'"
                    + ",'" + txtDenScurta.Text.Trim() + "'"
                    + ",'" + txtStrada.Text.Trim() + "'"
                    + ",'" + txtNr.Text.Trim() + "'"
                    + ",'" + txtBloc.Text.Trim() + "'"
                    + ",'" + txtScara.Text.Trim() + "'"
                    + ",'" + txtAp.Text.Trim() + "'"
                    + ",'" + txtLocalitate.Text.Trim() + "'"
                    + ",'" + txtJudet.Text.Trim() + "'"
                    + ",'" + txtCP.Text.Trim() + "'"
                    + ",'" + txtTelefon.Text.Trim() + "'"
                    + ",'" + txtFax.Text.Trim() + "'"
                    + ",'" + txtEmail.Text.Trim() + "'"
                    + ",'" + txtCF.Text.Trim() + "'"
                    + ",'" + txtCUI.Text.Trim() + "'"
                    + ",'" + txtBanca.Text.Trim() + "'"
                    + ",'" + txtIban.Text.Trim() + "'"
                    + ",'" + txtNumeRep.Text.Trim() + "'"
                    + ",'" + txtPrenumeRep.Text.Trim() + "'"
                    + ",'" + txtFunctie.Text.Trim() + "'"
                    + ",'" + txtCNP.Text.Trim() + "')"
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
