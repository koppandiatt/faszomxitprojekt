using Certificari.Classes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace Certificari.Views
{
    /// <summary>
    /// Interaction logic for Programe.xaml
    /// </summary>
    public partial class Programe : Window, INotifyPropertyChanged
    {
        private bool _isDetali = false;
        private bool _isEnabled = true;
        private string _isBtnVisibility = "Visible";

        private const string SALVEAZA = "Salvezaza";
        private const string ADAUGA = "Adauga";

        private int id = -1;

        private ICommMainUI contextMain;

        public Programe(bool isDet, DataRow detRow = null, ICommMainUI contextMain = null)
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
                id = (int)detRow["Id"];
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

        private void loadDatas(DataRow row)
        {
            try
            {

                txtProgram.Text = row["Denumire"] as string;
                txtSerie.Text = row["SerieA"] as string;
                txtNrA.Text = row["NrA"] as string;
                txtNrR.Text = row["NrR"] as string;
                txtCalificare.Text = row["Calificare"] as string;
                txtDomeniu.Text = row["Domeniu"] as string;
                txtCOR.Text = row["COR"] as string;
                txtAcces.Text = row["NivelAcces"] as string;
                txtNivel.Text = row["NivelCalificare"] as string;
                txtTipCert.Text = row["TipCalificare"] as string;
                txtTipProg.Text = row["TipProgram"] as string;
                txtTeoretic.Text = row["Teoretic"] as string;
                txtPractitc.Text = row["Practic"] as string;
                txtDurata.Text = row["Durata"] as string;
                txtDurataC.Text = row["DurataContract"] as string;
                txtValoare.Text = row["ValoareContract"] as string;
                txtTranse.Text = row["NumarTranse"] as string;
                txtCompetente.Text = row["Competente"] as string;

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



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string query = null;
            if (id < 0)
            {
                query = "INSERT INTO " + DAL.Tables.PROGRAME + " (" +
                "Denumire, SerieA, NrA, NrR," +
                "Calificare, Domeniu, COR, NivelAcces, NivelCalificare, TipCalificare," +
                "TipProgram, Teoretic, Practic, Durata, DurataContract, ValoareContract, NumarTranse, Competente ) VALUES ('" +
                txtProgram.Text  + "','" +
                txtSerie.Text + "','" +
                txtNrA.Text + "','" +
                txtNrR.Text  + "','" +
                txtCalificare.Text + "','" +
                txtDomeniu.Text  + "','" +
                txtCOR.Text  + "','" +
                txtAcces.Text + "','" +
                txtNivel.Text + "','" +
                txtTipCert.Text + "','" +
                txtTipProg.Text   + "','" +
                txtTeoretic.Text + "','" +
                txtPractitc.Text + "','" +
                txtDurata.Text + "','" +
                txtDurataC.Text   + "','" +
                txtValoare.Text + "','" +
                txtTranse.Text  + "','" +
                txtCompetente.Text +  "')";

            }
            else
            {
                query = " UPDATE " + DAL.Tables.PROGRAME + " SET " +
                         "Denumire = '" + txtProgram.Text +      
                         "', SerieA = '" + txtSerie.Text +       
                         "', NrA = '" + txtNrA.Text +
                         "', NrR = '" + txtNrR.Text +           
                         "', Calificare = '" + txtCalificare.Text +   
                         "', Domeniu = '" + txtDomeniu.Text +         
                         "', COR = '" + txtCOR.Text + 
                         "', NivelAcces = '" + txtAcces.Text +
                         "', NivelCalificare = '" + txtNivel.Text +
                         "', TipCalificare = '" + txtTipCert.Text +
                         "', TipProgram = '" + txtTipProg.Text +
                         "', Teoretic = '" + txtTeoretic.Text +
                         "', Practic = '" + txtPractitc.Text +
                         "', Durata = '" + txtDurata.Text +
                         "', DurataContract = '" + txtDurataC.Text +
                         "', ValoareContract = '" + txtValoare.Text +
                         "', NumarTranse = '" + txtTranse.Text +
                         "', Competente = '" + txtCompetente.Text + "' WHERE " +
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
