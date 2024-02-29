using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Windows;
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddProfessor.xaml
    /// </summary>
    public partial class AddProfessor : Window
    {
        public ProfessorDTO Professor { get; set; }

        private ProfessorController professorController;

        private AddressController addressController;

        public DateTime? dateTime { get; set; }
        public DateOnly Date;
        public AddProfessor(ProfessorController professorController, AddressController addressController)
        {
            InitializeComponent();
            DataContext = this;
            Professor = new ProfessorDTO();
            this.addressController = addressController;
            this.professorController = professorController;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Professor.IsValid && dateTime != null)
            {

                Address adresstoadd = addressController.getAddressById(Professor.AddressId);
                Professor professortoadd = professorController.getProfessorById(Professor.Id);
                if (adresstoadd != null)
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Adresa vec postoji.");

                    }
                    else
                    {
                        MessageBox.Show("Address with this id already exist.");

                    }

                    if (professortoadd != null)
                    {
                        MessageBox.Show("Proffesor with this id already exist.");
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                if (professortoadd != null)
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Profesor sa ovim brojem licne karte vec postoji.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Proffesor with this id already exist.");
                        return;
                    }


                }

                Professor.date = new DateOnly(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
                addressController.Add(Professor.ToAddress());
                professorController.Add(Professor.ToProfessor());



                Close();
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Profesor ne moze da se kreira.");
                    return;
                }
                else
                {
                    MessageBox.Show("Proffesor can't be created.");
                    return;
                }
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }


}

