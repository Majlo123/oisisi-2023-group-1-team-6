using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateProfessor.xaml
    /// </summary>
    public partial class UpdateProfessor : Window
    {
    
        
        public string Surname;

        public string Name;

        public DateOnly Date;
        public string street;
        public int number;
        public string city;
        public string state;
        public Address address;
        public string PhoneNumber;

        public string Email;

        public string Id;


        public string Title;
        public int WorkYear;
        public ProfessorDTO selected { get; set; }

        private ProfessorController professorController;
        public UpdateProfessor(ProfessorController professorcontroller,ProfessorDTO selectedProfessor)
        {
            InitializeComponent();
            selected = selectedProfessor;
            Surname = selected.Surname;
            Name = selected.Name;
            Date = selected.Date;


            street = selected.Street;
            number = selected.Number;
            city = selected.City;
            state = selected.State;
            Address address = new Address(street, number, city, state);

            PhoneNumber = selected.Phone;

            Email = selected.Email;
            Id = selected.Id;
            Title = selected.Title;
            WorkYear = selected.Workyear;
            DataContext = this;
            Professor professor=new Professor(Surname,Name,Date,address,PhoneNumber,Email,Id,Title,WorkYear);

            selected = new ProfessorDTO(professor, address);
            this.professorController = professorcontroller;

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (selected.IsValid)
            {
                professorController.Update(selected.ToProfessor());
                Close();
            }
            else
            {
                MessageBox.Show("Professor can not be updated. Not all fields are valid.");
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
