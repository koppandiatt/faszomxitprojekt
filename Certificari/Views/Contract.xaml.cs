using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Contract.xaml
    /// </summary>
    public partial class Contract : Window, INotifyPropertyChanged
    {

        private bool _isDetali = false;
        private bool _isEnabled = true;
        private string _isBtnVisibility = "Visible";
        private int id = -1;
        private const string SALVEAZA = "Salvezaza";
        private const string ADAUGA = "Adauga";

        private ICommMainUI contextMain;

        public Contract(bool isDet, DataRow detRow = null, ICommMainUI contextMain = null)
        {
            InitializeComponent();
            this.DataContext = this;
            isDetalii = isDet;
            isEnabled = !isDet;
            btnSaveContract.Content = ADAUGA;
            this.contextMain = contextMain;
            id = -1;
            if (detRow != null)
            {
                btnSaveContract.Content = SALVEAZA;
                id = (int)detRow["Id"];
                loadDatas(detRow);
            }
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

                txtNumar.Text = row["NrContract"] as string;
                txtNume.Text = row["Beneficiar"] as string;
                txtData.SelectedDate = DateTime.Parse((string)row["DataContract"]);
                txtValoarec.Text = row["ValoareContract"] as string;
                txtValoarel.Text = row["ValoareLitereContract"] as string;
                txtDurata.Text = row["DurataContract"] as string;
                txtMatricol.Text = row["NrMatricol"] as string;
                comboProgram.SelectedItem = row["ProgramContract"] as string;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare contract!");
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

        private void btnSaveContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = null;
                if (id < 0)
                {
                    query = "INSERT INTO Contracte (" +
                    "NrContract, Beneficiar, DataContract, ValoareContract, ValoareLitereContract, DurataContract, ProgramContract, NrMatricol) VALUES ('" +
                    txtNumar.Text + "','" +
                    txtNume.Text + "','" +
                    txtData.SelectedDate.Value.ToShortDateString() + "','" +
                    txtValoarec.Text + "','" +
                    txtValoarel.Text + "','" +
                    txtDurata.Text + "','" +
                    comboProgram.SelectedItem.ToString() + "','" +
                    txtMatricol.Text + "')";

                }
                else
                {
                    query = " UPDATE Contracte SET " +
                             "NrContract = '" + txtNumar.Text +
                             "', Beneficiar = '" + txtNume.Text +
                             "', DataContract = '" + txtData.SelectedDate.Value.ToShortDateString() +
                             "', ValoareContract = '" + txtValoarec.Text +
                             "', ValoareLitereContract = '" + txtValoarel.Text +
                             "', DurataContract = '" + txtDurata.Text +
                             "', ProgramContract = '" + comboProgram.SelectedItem.ToString() +
                             "', NrMatricol = '" + txtMatricol.Text + "' WHERE " +
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

    }
}
