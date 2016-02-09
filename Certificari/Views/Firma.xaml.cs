using Certificari.Classes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace Certificari.Views
{
    /// <summary>
    /// Interaction logic for Firma.xaml
    /// </summary>
    public partial class Firma : Window, INotifyPropertyChanged
    {

        private bool _isDetali = false;
        private bool _isEnabled = true;
        private string _isBtnVisibility = "Visible";

        private const string SALVEAZA = "Salvezaza";
        private const string ADAUGA = "Adauga";

        private int id = -1;

        private DataTable firmaTable = null;

        private ICommMainUI contextMain;

        public Firma(bool isDet, DataRow detRow = null, ICommMainUI contextMain = null)
        {
            InitializeComponent();
            this.DataContext = this;
            isDetalii = isDet;
            isEnabled = !isDet;
            btnSave.Content = ADAUGA;
            this.contextMain = contextMain;
            id = -1;
            if (detRow != null)
            {
                btnSave.Content = SALVEAZA;
                id =(int) detRow["Id"];
                loadDatas(detRow);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void loadDatas(DataRow row) {
            try
            {

                txtDenumire.Text = row["Denumire"] as string;
                txtStrada.Text = row["Strada"] as string;
                txtNr.Text = row["Nr"] as string;
                txtBloc.Text = row["Bloc"] as string;
                txtScara.Text = row["Scara"] as string;
                txtAp.Text = row["Ap"] as string;
                txtLocalitate.Text = row["Localitate"] as string;
                txtJudet.Text = row["Judet"] as string;
                txtCP.Text = row["cp"] as string;
                txtTelefon.Text = row["Telefon"] as string;
                txtFax.Text = row["Fax"] as string;
                txtEmail.Text = row["Email"] as string;
                txtSite.Text = row["Web"] as string;
                txtTribunal.Text = row["Tribunal"] as string;
                txtORC.Text = row["ORC"] as string;
                txtCF.Text = row["CodFiscal"] as string;
                txtCUI.Text = row["CUI"] as string;
                txtBanca.Text = row["Banca"] as string;
                txtIban.Text = row["IBAN"] as string;
                txtDriector.Text = row["Director"] as string;
                txtDirectorEc.Text = row["Director_economic"] as string;
                txtAltRepr.Text = row["Reprezentant"] as string;
                txtFunctie.Text = row["Functie"] as string;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare candidat!");
            }
  

        }

        public bool isEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged("isEnabled");
            }

        }

        public string isButtonsEnable
        {
            get
            {
                return _isBtnVisibility;
            }
            set
            {
                _isBtnVisibility = value;
                RaisePropertyChanged("isButtonsEnable");
            }
        }

        public bool isDetalii
        {
            get
            {
                return _isDetali;
            }
            set
            {
                _isDetali = value;
                if (_isDetali) isButtonsEnable = "Collapsed";
                else isButtonsEnable = "Visible";
               
                RaisePropertyChanged("isDetalii");
                
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string query = null;
            if (id < 0)
            {
                  query = "INSERT INTO "+ DAL.Tables.FIRME +" (" +
                  "Denumire, Strada, Nr, Bloc," +
                  "Scara, Ap, Localitate, Judet, cp, Telefon," +
                  "Fax, Email, Web, Tribunal, ORC, CodFiscal, CUI, Banca, IBAN, Director, Director_economic, Reprezentant, Functie) VALUES ('" +
                  txtDenumire.Text + "','" +
                  txtStrada.Text + "','" +
                  txtNr.Text + "','" +
                  txtBloc.Text + "','" +
                  txtScara.Text + "','" +
                  txtAp.Text + "','" +
                  txtLocalitate.Text + "','" +
                  txtJudet.Text + "','" +
                  txtCP.Text + "','" +
                  txtTelefon.Text + "','" +
                  txtFax.Text + "','" +
                  txtEmail.Text + "','" +
                  txtSite.Text + "','" +
                  txtTribunal.Text + "','" +
                  txtORC.Text + "','" +
                  txtCF.Text + "','" +
                  txtCUI.Text + "','" +
                  txtBanca.Text + "','" +
                  txtIban.Text + "','" +
                  txtDriector.Text + "','" +
                  txtDirectorEc.Text + "','" +
                  txtAltRepr.Text + "','" +
                  txtFunctie.Text + "')";

            }
            else
            {
                query = " UPDATE " + DAL.Tables.FIRME + " SET " +
                         "Denumire = '" + txtDenumire.Text +
                         "', Strada = '" + txtStrada.Text +
                         "', Nr = '" + txtNr.Text +
                         "', Bloc = '" + txtBloc.Text +
                         "', Scara = '" + txtScara.Text +
                         "', Ap = '" + txtAp.Text +
                         "', Localitate = '" + txtLocalitate.Text +
                         "', Judet = '" + txtJudet.Text +
                         "', cp = '" + txtCP.Text +
                         "', Telefon = '" + txtTelefon.Text +
                         "', Fax = '" + txtFax.Text +
                         "', Email = '" + txtEmail.Text +
                         "', Web = '" + txtSite.Text +
                         "', Tribunal = '" + txtTribunal.Text +
                         "', ORC = '" + txtORC.Text +
                         "', CodFiscal = '" + txtCF.Text +
                         "', CUI = '" + txtCUI.Text +
                         "', Banca = '" + txtBanca.Text +
                         "', IBAN = '" + txtIban.Text +
                         "', Director = '" + txtDriector.Text +
                         "', Director_economic = '" + txtDirectorEc.Text +
                         "', Reprezentant = '" + txtAltRepr.Text +
                         "', Functie = '" + txtFunctie.Text + "' WHERE " +
                         "Id = " + id;
         
            }
           

            if (DAL.getInstance().InsertUpdate(query) == "Success")
            {
                if (contextMain == null) return;
                contextMain.updateOperatii();
                this.Close();
            }
            else
            {
                labelError.Content = "Erroare la inserarea datelor in baza de date";
            }
            
        }
    }
}
