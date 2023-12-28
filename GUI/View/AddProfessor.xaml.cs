using System;
using System.Collections.Generic;
using System.Globalization;
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
using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;

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
        public AddProfessor(ProfessorController professorController,AddressController addressController)
        {
            InitializeComponent();
            DataContext = this;
            Professor = new ProfessorDTO();
            this.addressController = addressController;
            this.professorController = professorController;
          
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Professor.IsValid)
            {
                addressController.Add(Professor.ToAddress());
                professorController.Add(Professor.ToProfessor());
                //string street, int number, string city, string state

                
                Close();
            }
            else
            {
                MessageBox.Show("Professor can not be created. Not all fields are valid.");
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }

    
}

