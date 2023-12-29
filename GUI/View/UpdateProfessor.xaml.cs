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
        public int addressid;
        public string street;
        public int number;
        public string city;
        public string state;
        public Address address;
        public string PhoneNumber;
        public DateTime dateTime { get; set; }

        public string Email;

        public string Id;


        public string Title;
        public int WorkYear;
        public ProfessorDTO professor { get; set; }

        private ProfessorController professorController;
        private AddressController addressController;
        public UpdateProfessor(ProfessorController professorcontroller,ProfessorDTO selectedProfessor,AddressController addressController)
        {
            InitializeComponent();
            this.professor = selectedProfessor;
            Surname = this.professor.Surname;
            Name = this.professor.Name;
            Date = (DateOnly)this.professor.Date;

            addressid = this.professor.AddressId;
            street = this.professor.Street;
            number = this.professor.Number;
            city = this.professor.City;
            state = this.professor.State;
            Address address = new Address(addressid,street, number, city, state);
            this.dateTime = new DateTime(Date.Year, Date.Month, Date.Day);

            PhoneNumber = this.professor.Phone;

            Email = this.professor.Email;
            Id = this.professor.Id;
            Title = this.professor.Title;
            WorkYear = this.professor.Workyear;
            DataContext = this;
            //Professor professor=new Professor(Surname,Name,Date,address,PhoneNumber,Email,Id,Title,WorkYear);

            //this.professor = new ProfessorDTO(professor, address);
            this.professorController = professorcontroller;
            this.addressController = addressController;

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (professor.IsValid)
            {
                professor.date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                addressController.Update(professor.ToAddress());
                professorController.Update(professor.ToProfessor());
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
