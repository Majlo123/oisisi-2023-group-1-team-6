using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public MainWindow SelectedStudent { get; set; }
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

        public int WorkYear;
        public StudentDTO student { get; set; }

        public StudentDTO selected;

        public SubjectDTO subject { get; set; }
        private ProfessorController professorController;
        private AddressController addressController;
        private StudentController studentController;
        private SubjectController subjectController;
        private IndexController indexController;
        private GradeController gradeController;
        private StudentSubjectController studentSubjectController;
        public StudentSubject studentSubject { get; set; }
        public ObservableCollection<SubjectDTO> UnpassedSubjects { get; set; }
        public GradeDTO grade { get; set; }
        public ObservableCollection<GradeDTO> PassedSubjects { get; set; }
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
            studentSubjectController = new StudentSubjectController();
            gradeController = new GradeController();
            UnpassedSubjects = new ObservableCollection<SubjectDTO>(
            studentSubjectController.GetAllSubjectsById(student.id)
            .Select(subject => new SubjectDTO(subject))
            .ToList());
            PassedSubjects = new ObservableCollection<GradeDTO>(
            gradeController.GetAllGradesByStudent(student.id)
            .Select(grade => new GradeDTO(grade))
            .ToList());
            subject = new SubjectDTO();
            studentSubject = new StudentSubject();
            Update();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (student.IsValid)
            {
                student.date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                indexController.Update(student.ToIndex());
                addressController.Update(student.ToAddress());
                studentController.Update(student.toStudent());
                Close();
            }
            else
            {
                MessageBox.Show("Student can not be updated. Not all fields are valid.");
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Update()
        {

            UnpassedSubjects.Clear();
            foreach (CLI.Model.Subject subject in studentSubjectController.GetAllSubjectsById(student.id))

                UnpassedSubjects.Add(new SubjectDTO(subject));
            PassedSubjects.Clear();
            foreach (Grade grade in gradeController.GetAllGradesByStudent(student.id)) { 
                PassedSubjects.Add(new GradeDTO(grade));
            }

        }
        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {


            AddSubjectToStudent addSubject = new AddSubjectToStudent(student);
            addSubject.Closed += AddSubject_Closed;
            addSubject.Show();




        }
        private void Take_Exam_Click(object sender, RoutedEventArgs e)
        {
            if (subject != null)
            {
                TakeExam takeexam = new TakeExam(subject, student);
                takeexam.Show();

            }
            else
            {
                MessageBox.Show("Please select subject that you want to take exam.");
            }


        }
        private void AddSubject_Closed(object sender, EventArgs e)
        {
            Update();
        }
        private void DeleteSubject_Click(Object sender, RoutedEventArgs e)
        {
            if (subject == null)
            {

                MessageBox.Show("Please select subject that you want to delete from student.");
            }
            else
            {
                string message = "Are you sure that you want to delete a subject?";
                string title = "Deleting subject from student";

                MessageBoxResult result =
                 MessageBox.Show(message, title,
       MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {

                    studentSubjectController.Delete(subject.subjectId);
                    Update();

                }

                else
                { }
            }

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        private void DeleteSubjectPassed_Click(object sender, TextChangedEventArgs e)
        {

        }
        
        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

