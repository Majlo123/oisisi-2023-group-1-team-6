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
using Index = StudentskaSluzba.Model.Index;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateProfessor.xaml
    /// </summary>
    public partial class UpdateStudent : Window
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
        public int idIndex;
        public String abb;
        public int mark;
        public int year;
        public DateTime dateTime { get; set; }
        public string Email;
        public Index index;

        public int Id;


        public string Title;
        public int WorkYear;
        public StudentDTO student { get; set; }

        private ProfessorController professorController;
        private AddressController addressController;
        private StudentController studentController;
        private IndexController indexController;
        public UpdateStudent(StudentController studentController, StudentDTO selectedStudent, AddressController addressController, IndexController indexController)
        {
            InitializeComponent();
            this.student = selectedStudent;
            Surname = this.student.Surname;
            Name = this.student.Name;
            Date = (DateOnly)this.student.Date;

            addressid = this.student.IdAddress;
            street = this.student.Street;
            number = this.student.Number;
            city = this.student.City;
            state = this.student.State;
            Address address = new Address(addressid, street, number, city, state);
            this.dateTime = new DateTime(Date.Year, Date.Month, Date.Day);
            PhoneNumber = this.student.Phonenumber;

            idIndex = this.student.IdIndex;
            abb = this.student.AbbreviationOfMajor;
            mark = this.student.MarkOfMajor;
            year = this.student.yearOfEnrollment;
            Index index = new Index(idIndex, abb, mark, year);
            Email = this.student.Email;
            Id = this.student.Id;
            DataContext = this;
           // Student student = new Student(Surname, Name, Date, address, PhoneNumber, Email, Id, Title, WorkYear);

           // this.student = new StudentDTO(student, address, index); ;
            this.studentController = studentController;
            this.addressController = addressController;
            this.indexController = indexController;

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (student.IsValid)
            {
                student.date = new DateOnly(dateTime.Year,dateTime.Month, dateTime.Day);
                indexController.Update(student.ToIndex());
                addressController.Update(student.ToAddress());
                studentController.Update(student.toStudent());
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

