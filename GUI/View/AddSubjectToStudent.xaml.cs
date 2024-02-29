using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddSubjectToStudent.xaml
    /// </summary>
    public partial class AddSubjectToStudent : Window, IObserver
    {


        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public SubjectController subject_controler;
        public StudentController studentController;
        public StudentSubjectController studentsubject_controler;
        public AddressController addressController;
        public IndexController indexController;

        public UpdateStudent updateStudent;
        public SubjectDTO selectedSubject { get; set; }
        public StudentDTO selectedStudent { get; set; }
        public AddSubjectToStudent(StudentDTO student)
        {
            InitializeComponent();
            subject_controler = new SubjectController();
            studentsubject_controler = new StudentSubjectController();
            DataContext = this;
            studentController = new StudentController();
            addressController = new AddressController();
            indexController = new IndexController();
            selectedStudent = student;
            updateStudent = new UpdateStudent(studentController, selectedStudent, addressController, indexController);
            subject_controler.Subscribe(this);
            studentsubject_controler.Subscribe(this);
            Subjects = new ObservableCollection<SubjectDTO>();
            Update();
        }


        public void Update()
        {
            Subjects.Clear();

            foreach (CLI.Model.Subject subject in subject_controler.GetAllSubjects())
            {
                GradeDTO grade = updateStudent.PassedSubjects.FirstOrDefault(g => g.subjectId == subject.subjectId);
                SubjectDTO subject1 = updateStudent.UnpassedSubjects.FirstOrDefault(s => s.SubjectId == subject.subjectId);

                if (grade == null && subject1 == null && selectedStudent.yearOfStudy >= subject.yearOfStudy)
                {
                    Subjects.Add(new SubjectDTO(subject));
                }
            }


        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (selectedSubject != null)
            {
                studentsubject_controler.Add(selectedStudent.id, selectedSubject.subjectId);

                Close();
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberi predmet koji zelis da dodas.");
                }
                else
                {
                    MessageBox.Show("Chose subject that you want to add.");
                }

            }

        }
    }
}
