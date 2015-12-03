﻿using Certificari.Classes;
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
        



        public Candidat(bool isDet,DataRow detRow = null)
        {
            InitializeComponent();
            this.DataContext = this;
            isDetalii = isDet;
            isEnabled = !isDet;
            if (isDetalii && detRow != null)
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured in detalii page!");
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
            string query = "INSERT INTO Candidat (" +
                "NrMatricol, Nume, Prenume, Tata, Mama, CNP, DataNasterii, LoculNasterii, Strada, Nr, Bloc," +
                "Scara, Ap, Localitate, Judet, Cp, Telefon, Studii, Profesia, LocMunca) VALUES ('" +
                txtNumar.Text + "','" +
                txtNume.Text + "','" +
                txtPrenume.Text + "','" +
                txtTata.Text + "','" +
                txtMama.Text + "','" +
                txtCNP.Text + "','" +
                txtDataNasterii.SelectedDate.Value.ToShortDateString() +"','" +
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

            if (DAL.getInstance().InsertUpdate(query) == "Success")
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
