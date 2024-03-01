using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateProfessor.xaml
    /// </summary>
    public partial class UpdateProfessor : Window
    {


        public string Surname;

        public string Namee;

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


        public string Titlee;
        public int WorkYear;
        public ProfessorDTO professor { get; set; }
        public SubjectDTO subject { get; set; }

        private ProfessorController professorController;
        private AddressController addressController;
        private ProfessorSubjectController professorSubjectController;
        private StudentSubjectController studentSubjectController;
        private IndexController indexController;

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public UpdateProfessor(ProfessorController professorcontroller, ProfessorDTO selectedProfessor, AddressController addressController)
        {
            InitializeComponent();
            this.professor = selectedProfessor;
            Surname = this.professor.Surname;
            Namee = this.professor.Name;
            Date = (DateOnly)this.professor.Date;

            addressid = this.professor.AddressId;
            street = this.professor.Street;
            number = this.professor.Number;
            city = this.professor.City;
            state = this.professor.State;
            Address address = new Address(addressid, street, number, city, state);
            this.dateTime = new DateTime(Date.Year, Date.Month, Date.Day);

            PhoneNumber = this.professor.Phone;

            Email = this.professor.Email;
            Id = this.professor.Id;
            Titlee = this.professor.Title;
            WorkYear = this.professor.Workyear;
            DataContext = this;
            //Professor professor=new Professor(Surname,Name,Date,address,PhoneNumber,Email,Id,Title,WorkYear);

            //this.professor = new ProfessorDTO(professor, address);
            professorSubjectController = new ProfessorSubjectController();
            Subjects = new ObservableCollection<SubjectDTO>(
            professorSubjectController.GetAllSubjectsById(professor.Id)
            .Select(subject => new SubjectDTO(subject))
            .ToList());
            this.professorController = professorcontroller;
            this.addressController = addressController;
            studentSubjectController = new StudentSubjectController();
            indexController = new IndexController();
            Update();
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
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Profesor ne moze da se azurira. Nisu sva polja validno populjena.");
                }
                else
                {
                    MessageBox.Show("Professor can not be updated. Not all fields are valid.");
                }

            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            if (subject == null)
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite predmet koji zelite da obrise od profesora.");
                }
                else
                {
                    MessageBox.Show("Please select subject that you want to delete from professor.");
                }

            }
            else
            {
                string message = "";
                string title = "";
                if (GlobalData.SharedString == "sr-RS")
                {
                    message = "Da li ste sigurni da zelite da obrisete predmet?";
                    title = "Brisanje predmeta sa profesora";
                }
                else
                {
                    message = "Are you sure that you want to delete a subject?";
                    title = "Deleting subject from professor";
                }

                MessageBoxResult result =
                 MessageBox.Show(message, title,
       MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {

                    professorSubjectController.Delete(subject.subjectId);
                    Update();

                }

                else
                { }
            }
        }
        
        private void ShowStudents_Click(object sender, RoutedEventArgs e)
        {
            List<StudentDTO> Students = new List<StudentDTO>();

            var subjectsProfessorTeaches = professorSubjectController.GetAllSubjectsById(professor.Id);
            foreach(Subject sub in subjectsProfessorTeaches)
            {
                var studentsWhoListenTheseSubjects = studentSubjectController.getAllStudentsBySubjectId(sub.subjectId);
                foreach(Student stud in studentsWhoListenTheseSubjects)
                { 
                    StudentDTO stud2 = Students.FirstOrDefault(ss => ss.id == stud.Id);
                    if(stud2 == null)
                    {
                        Address studentAddress = addressController.getAddressById(stud.Address.id);
                        StudentskaSluzba.Model.Index studentIndex = indexController.getIndexById(stud.Index.IdIndex);
                        Students.Add(new StudentDTO(stud, studentAddress, studentIndex));
                    }
                }
            }

            ShowStudentsWindow showStudents = new ShowStudentsWindow(Students);
            showStudents.Show();
        }

        private void Update()
        {

            Subjects.Clear();
            foreach (StudentskaSluzba.Model.Subject subject in professorSubjectController.GetAllSubjectsById(professor.Id))
                Subjects.Add(new SubjectDTO(subject));


        }

        private void AddProfessor_Closed(object sender, EventArgs e)
        {
            Update();
        }

        private void AddSubject_Click(object sender, EventArgs e)
        {
            AddSubjectToProfessor addProfessor = new AddSubjectToProfessor(professor);
            addProfessor.Closed += AddProfessor_Closed;
            addProfessor.Show();
        }
    }
}
