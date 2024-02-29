using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static GUI.MainWindow;
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

        public string Namee;

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
        public int espb;
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
            Namee = this.student.Name;
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
            subjectController = new SubjectController();
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
            professorController = new ProfessorController();
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
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Student ne moze da se azurira. Nisu sva polja dobro popunjena");
                }
                else
                {
                    MessageBox.Show("Student can not be updated. Not all fields are valid.");
                }



            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Update()
        {

            UnpassedSubjects.Clear();
            foreach (CLI.Model.Subject subject in studentSubjectController.GetAllSubjectsById(student.id))
            {
                CLI.Model.Subject subjecttmp = subjectController.getSubjectById(subject.subjectId);
                if (subjecttmp.semester == "Winter" || subjecttmp.semester == "Zimski")
                {

                    if (GlobalData.SharedString == "sr-RS")
                    {
                        subjecttmp.semester = "Zimski";
                    }
                    else
                    {
                        subjecttmp.semester = "Winter";
                    }
                }
                else
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        subjecttmp.semester = "Letnji";
                    }
                    else
                    {
                        subjecttmp.semester = "Summer";
                    }

                }
                UnpassedSubjects.Add(new SubjectDTO(subjecttmp));
            }


            PassedSubjects.Clear();
            foreach (Grade grade in gradeController.GetAllGradesByStudent(student.id))
            {
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
            if (subject.subjectName != null)
            {
                TakeExam takeexam = new TakeExam(subject, student);
                takeexam.Show();

            }

            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite predmet koji zelite da polazete.");
                }
                else
                {
                    MessageBox.Show("Please select subject that you want to take exam.");
                }

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

                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite predmet koji zelite da obrisete studentu");
                }
                else
                {
                    MessageBox.Show("Please select subject that you want to delete from student.");
                }
            }
            else
            {
                string message = "";
                string title = "";
                if (GlobalData.SharedString == "sr-RS")
                {
                    message = "Da li ste sigurni da hocete da obrisete predmet?";
                    title = "Brisanje predmeta sa studenta";
                }
                else
                {
                    message = "Are you sure that you want to delete a subject?";
                    title = "Deleting subject from student";
                }

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

        private void DeletePassedSubject_Click(object sender, RoutedEventArgs e)
        {
            if (subject == null)
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite predmet koji zelite da obrisete studentu");
                }
                else
                {
                    MessageBox.Show("Please select subject that you want to delete from student.");
                }

            }
            else
            {
                string message = "";
                string title = "";
                if (GlobalData.SharedString == "sr-RS")
                {
                    message = "Da li ste sigurni da hocete da obrisete predmet?";
                    title = "Brisanje predmeta sa studenta";
                }
                else
                {
                    message = "Are you sure that you want to delete a subject?";
                    title = "Deleting subject from student";
                }
                MessageBoxResult result =
                 MessageBox.Show(message, title,
       MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    studentSubjectController.Add(student.id, grade.subjectId);
                    gradeController.Delete(subject.subjectId);
                    Update();

                }

                else
                { }
            }
        }
        private void Show_Professors_Click(object sender, RoutedEventArgs e)
        {
            if (student != null)
            {
                // Get all subjects for the selected student
                List<SubjectDTO> subjects = new List<SubjectDTO>();
                foreach (CLI.Model.Subject subject in studentSubjectController.GetAllSubjectsById(student.id))
                {
                    subjects.Add(new SubjectDTO(subject));
                }




                List<string> professorIds = subjects
                        .Select(subject => subject.professor)
                        .Distinct()
                        .ToList();
                HashSet<string> uniqueProfessorIds = new HashSet<string>();

                if (professorIds.Count > 0)
                {
                    List<ProfessorDTO> professors = new List<ProfessorDTO>();
                    foreach (string professorId in professorIds)
                    {
                        if (uniqueProfessorIds.Add(professorId))
                        {
                            // If the professor ID is added to the HashSet (i.e., it's unique), get the professor
                            List<Professor> professorList = professorController.GetProfessorsById(professorId);

                            foreach (Professor professor in professorList)
                            {
                                professors.Add(new ProfessorDTO(professor));
                            }
                        }
                    }

                    if (professors.Count > 0)
                    {
                        ShowProfessorsWindow showProfessorsWindow = new ShowProfessorsWindow(professors);
                        showProfessorsWindow.ShowDialog();
                    }
                    else
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            MessageBox.Show("Ni jedan profesor nije nadjen za predmet koji ima student");
                        }
                        else
                        {
                            MessageBox.Show("No professors found for the subjects associated with this student.");
                        }
                    }
                }
                else
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Ni jedan profesor nije nadjen za predmet koji ima student");
                    }
                    else
                    {
                        MessageBox.Show("No professors found for the subjects associated with this student.");
                    }
                }
            }
            else
            {

                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Student nema ni jedan predmet.");
                }
                else
                {
                    MessageBox.Show("This student is not enrolled in any subjects.");
                }

            }


        }

        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}

