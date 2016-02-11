using Certificari.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Candidat.xaml
    /// </summary>
    public partial class Candidat : Window, INotifyPropertyChanged
    {

        private bool _isDetali = false;
        private bool _isEnabled = true;
        private string _isBtnVisibility = "Visible";

        private const string SALVEAZA = "Salvezaza";
        private const string ADAUGA = "Adauga";

        private int id = -1;
        private int idce = -1; 
        private int idc = -1;

        private List<Incasari> list;

        private ICommMainUI contextMain;

        public Candidat(bool isDet,DataRow detRow = null,ICommMainUI contextMain = null)
        {
            InitializeComponent();
            this.DataContext = this;
            isDetalii = isDet;
            isEnabled = !isDet;
            btnAdd.Content = ADAUGA;
            this.contextMain = contextMain;
            id = -1;
            if (detRow != null)
            {
                btnAdd.Content = SALVEAZA;
                id =(int) detRow["Id"];
                loadDatas(detRow);
            }
            else
            {
                tabContract.IsEnabled = false;
                tabIncasari.IsEnabled = false;
                tabPrograme.IsEnabled = false;
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
            
            
            DataTable dt = new DataTable();
            try
            {
                dt = DAL.getInstance().select("Select * FROM Programe WHERE Status=1");
                if (dt != null)
                {
                    Console.WriteLine("Not null");
                    foreach (DataRow key in dt.Rows)
                    {
                        Console.WriteLine(key);
                        comboProgram.Items.Add(key["Denumire"] as string);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {

                txtNumar.Text = row["NrMatricol"] as string;
                txtNume.Text = row["Nume"] as string;
                txtPrenume.Text = row["Prenume"] as string;
                txtTata.Text = row["Tata"] as string;
                txtMama.Text = row["Mama"] as string;
                txtCNP.Text = row["CNP"] as string;
                Console.WriteLine("nasteri" + (string)row["DataNasterii"]);
                txtDataNasterii.SelectedDate = DateTime.Parse((string) row["DataNasterii"]);
                txtLoculN.Text = row["LoculNasterii"] as string;
                txtStrada.Text = row["Strada"] as string;
                txtNr.Text = row["Nr"] as string;
                txtBloc.Text = row["Bloc"] as string;
                txtScara.Text = row["Scara"] as string;
                txtAp.Text = row["Ap"] as string;
                txtLocalitatea.Text = row["Localitate"] as string;
                txtJudet.Text = row["Judet"] as string;
                txtCP.Text = row["Cp"] as string;
                txtTelefon.Text = row["Telefon"] as string;
                txtStudii.Text = row["Studii"] as string;
                txtProfesia.Text = row["Profesia"] as string;
                txtLocMunca.Text = row["LocMunca"] as string;

                DataRow rowP = null;
                DataTable Program = DAL.getInstance().select("SELECT * FROM ProgramCandidat WHERE IdCandidat=" + id);
                if (Program.Rows.Count > 0 && Program.Rows != null)
                {

                    rowP = Program.Rows[0];
                    idc = Convert.ToInt32(rowP["IdCandidat"] as string);
                    Console.WriteLine("Here {0}, {1}", idc, rowP["IdCandidat"] as string);
                    datePregatireS.SelectedDate = DateTime.Parse((string)rowP["PpregDin"]);
                    datePregatireE.SelectedDate = DateTime.Parse((string)rowP["PpregPana"]);
                    dateTeoreticaS.SelectedDate = DateTime.Parse((string)rowP["PteorDin"]);
                    dateTeoreticaE.SelectedDate = DateTime.Parse((string)rowP["PteorPana"]);
                    datePracticaS.SelectedDate = DateTime.Parse((string)rowP["PpracDin"]);
                    datePracticaE.SelectedDate = DateTime.Parse((string)rowP["PpracPana"]);
                    dateAbsolvire.SelectedDate = DateTime.Parse((string)rowP["Absolvire"]);
                    txtMediaGenerala.Text = rowP["Media"] as string;
                    txtCalificativ.Text = rowP["Calificativ"] as string;
                    comboProgram.SelectedItem = rowP["Program"] as string;
                }

                list = new List<Incasari>();
                DataTable dtIncasari = DAL.getInstance().select("SELECT * FROM Incasari WHERE IdCandidat=" + id);
                if (dtIncasari.Rows.Count > 0 && dtIncasari.Rows != null)
                {
                    foreach (DataRow rowI in dtIncasari.Rows)
                    {
                        Incasari inc = new Incasari();
                        inc.Data = rowI["Data"] as string;
                        inc.Valoare = rowI["Valoare"] as string + " RON";
                        list.Add(inc);
                    }
                    listIncasari.ItemsSource = list;
                }

                DataRow rowC = null;
                DataTable Certificat = DAL.getInstance().select("SELECT * FROM Certificat WHERE IdCandidat=" + id);
                if (Certificat.Rows.Count > 0 && Certificat.Rows != null)
                {
                    idce = 1;
                    rowC = Certificat.Rows[0];
                    txtNrOrdine.Text = rowC["NrOrdine"] as string;
                    txtSerie.Text = rowC["Serie"] as string;
                    txtNumarCert.Text = rowC["Numar"] as string;
                    dateEliberare.SelectedDate = DateTime.Parse((string)rowC["DataEliberarii"]);
                    txtObservatii.Text = rowC["Observatii"] as string;

                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare candidat!\n "+ ex.Message);
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
            if (txtNumar.Text != "" || txtNume.Text != "" || txtPrenume.Text != "" || txtCNP.Text != "" || txtDataNasterii.Text != "")
            {
                string query = null;
                if (id < 0)
                {
                    query = "INSERT INTO " + DAL.Tables.CANDIDAT + " (" +
                    "NrMatricol, Nume, Prenume, Tata, Mama, CNP, DataNasterii, LoculNasterii, Strada, Nr, Bloc," +
                    "Scara, Ap, Localitate, Judet, Cp, Telefon, Studii, Profesia, LocMunca) VALUES ('" +
                    txtNumar.Text + "','" +
                    txtNume.Text + "','" +
                    txtPrenume.Text + "','" +
                    txtTata.Text + "','" +
                    txtMama.Text + "','" +
                    txtCNP.Text + "','" +
                    txtDataNasterii.SelectedDate.Value.ToShortDateString() + "','" +
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

                }
                else
                {
                    query =  " UPDATE " + DAL.Tables.CANDIDAT + " SET " +
                             "NrMatricol = '" + txtNumar.Text +
                             "', Nume = '" + txtNume.Text +
                             "', Prenume = '" + txtPrenume.Text +
                             "', Tata = '" + txtTata.Text +
                             "', Mama = '" + txtMama.Text +
                             "', CNP = '" + txtCNP.Text +
                             "', DataNasterii = '" + txtDataNasterii.SelectedDate.Value.ToShortDateString() +
                             "', LoculNasterii = '" + txtLoculN.Text +
                             "', Strada = '" + txtStrada.Text +
                             "', Nr = '" + txtNr.Text +
                             "', Bloc = '" + txtBloc.Text +
                             "', Scara = '" + txtScara.Text +
                             "', Ap = '" + txtAp.Text +
                             "', Localitate = '" + txtLocalitatea.Text +
                             "', Judet = '" + txtJudet.Text +
                             "', CP = '" + txtCP.Text +
                             "', Telefon = '" + txtTelefon.Text +
                             "', Studii = '" + txtStudii.Text +
                             "', Profesia = '" + txtProfesia.Text +
                             "', LocMunca = '" + txtLocMunca.Text + "' WHERE " +
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
            else
            {
                labelError.Content = "Va rugam completati campurile!";
            }
            
        }

        private void btnSaveProgram_Click(object sender, RoutedEventArgs e)
        {
            string query = null;
            if (idc < 0)
            {
                query = "INSERT INTO ProgramCandidat (" +
                "IdCandidat, Program, PpregDin, PpregPana, PteorDin, PteorPana, PpracDin, PpracPana, Absolvire, " +
                "Media, Calificativ) VALUES (" +
                id + " ,'" +
                comboProgram.SelectedItem.ToString() + "','" +
                datePregatireS.SelectedDate.Value.ToShortDateString() + "','" +
                datePregatireE.SelectedDate.Value.ToShortDateString() + "','" +
                dateTeoreticaS.SelectedDate.Value.ToShortDateString() + "','" +
                dateTeoreticaE.SelectedDate.Value.ToShortDateString() + "','" +
                datePracticaS.SelectedDate.Value.ToShortDateString() + "','" +
                datePracticaE.SelectedDate.Value.ToShortDateString() + "','" +
                dateAbsolvire.SelectedDate.Value.ToShortDateString() + "','" +
                txtMediaGenerala.Text + "','" +
                txtCalificativ.Text + "')";

            }
            else
            {
                query = " UPDATE ProgramCandidat SET " +
                         "Program = '" + comboProgram.SelectedItem.ToString() +
                         "', PpregDin = '" + datePregatireS.SelectedDate.Value.ToShortDateString() +
                         "', PpregPana = '" + datePregatireE.SelectedDate.Value.ToShortDateString() +
                         "', PteorDin = '" + dateTeoreticaS.SelectedDate.Value.ToShortDateString() +
                         "', PteorPana = '" + dateTeoreticaE.SelectedDate.Value.ToShortDateString() +
                         "', PpracDin = '" + datePracticaS.SelectedDate.Value.ToShortDateString() +
                         "', PpracPana = '" + datePracticaE.SelectedDate.Value.ToShortDateString() +
                         "', Absolvire = '" + dateAbsolvire.SelectedDate.Value.ToShortDateString() +
                         "', Media = '" + txtMediaGenerala.Text +
                         "', Calificativ = '" + txtCalificativ.Text + "' WHERE " +
                         "IdCandidat = " + id;

            }

            if (DAL.getInstance().InsertUpdate(query) == "Success")
            {
                if (contextMain == null) return;
                contextMain.updateOperatii();
            }
            else
            {
                labelError.Content = "Erroare la inserarea datelor in baza de date";
            }
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            string query = null;
            
            query = "INSERT INTO Incasari (" +
                "Data, Valoare, IdCandidat) VALUES ('" +
                dateIncasare.SelectedDate.Value.ToShortDateString() + "','" +
                txtValoare.Text + "', " +
                id + ")";
            if (DAL.getInstance().InsertUpdate(query) == "Success")
            {
                if (contextMain == null) return;
                Incasari inc = new Incasari();
                inc.Data = dateIncasare.SelectedDate.Value.ToShortDateString();
                inc.Valoare = txtValoare.Text;
                list.Add(inc);
                listIncasari.ItemsSource = null;
                listIncasari.ItemsSource = list;
            }
            else
            {
                labelError.Content = "Erroare la inserarea datelor in baza de date";
            }    
            
        }

        public class Incasari
        {
            public string Data { get; set; }
            public string Valoare { get; set; }
        }

        private void btnSaveCertificat_Click(object sender, RoutedEventArgs e)
        {
            string query = null;
            if (idce < 0)
            {
                query = "INSERT INTO Certificat (" +
                "IdCandidat, NrOrdine, Serie, Numar, DataEliberarii, Observatii) VALUES (" +
                id + " ,'" +
                txtNrOrdine.Text + "','" +
                txtSerie.Text + "','" +
                txtNumarCert.Text + "' ,'" +
                dateEliberare.SelectedDate.Value.ToShortDateString() + "' ,'" +
                txtObservatii.Text + "')";
            }
            else
            {
                query = " UPDATE Certificat SET " +
                         "NrOrdine = '" + txtNrOrdine.Text +
                         "', Serie = '" + txtSerie.Text +
                         "', Numar = '" + txtNumarCert.Text +
                         "', DataEliberarii = '" + dateEliberare.SelectedDate.Value.ToShortDateString() +
                         "', Observatii = '" + txtObservatii.Text + "' WHERE " +
                         "IdCandidat = " + id;

            }

            if (DAL.getInstance().InsertUpdate(query) == "Success")
            {
                if (contextMain == null) return;
                contextMain.updateOperatii();
            }
            else
            {
                labelError.Content = "Erroare la inserarea datelor in baza de date";
            }

            
        }

        private void btnSaveListare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveListareFata_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveListareSpate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
