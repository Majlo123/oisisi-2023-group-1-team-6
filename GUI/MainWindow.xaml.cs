using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IObserver
    {
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        public StudentDTO SelectedStudent {  get; set; }

        public SubjectDTO SelectedSubject { get; set; }
        private ProfessorController professorsController { get; set; }
        private SubjectController subjectsController { get; set; }
        private StudentController studentController {  get; set; }
        private AddressController addressController { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Professors = new ObservableCollection<ProfessorDTO>();
            Students = new ObservableCollection<StudentDTO>();
            Subjects = new ObservableCollection<SubjectDTO>();
            professorsController = new ProfessorController();
            addressController = new AddressController();
            studentController = new StudentController();
            subjectsController = new SubjectController();
            addressController.Subscribe(this);
            professorsController.Subscribe(this);
            subjectsController.Subscribe(this);
            Update();
        }
        private void Add_Click_Professor(object sender, RoutedEventArgs e)
        {
            AddProfessor addProffessor = new AddProfessor(professorsController);
            addProffessor.Show();
        }

        private void Add_Click_Student(object sender, RoutedEventArgs e)
        {
            AddStudent addStudent = new AddStudent(studentController);
            addStudent.Show();
        }
        private void Add_Click_Subject(object sender, RoutedEventArgs e)
        {
            AddSubject addSubject = new AddSubject(subjectsController);
            addSubject.Show();
        }

        public void Update()
        {
            Subjects.Clear();
            Professors.Clear();
            
            foreach (Address address in addressController.GetAllAddress())
            foreach (Professor professor in professorsController.GetAllProfessors()) Professors.Add(new ProfessorDTO(professor,address));

            foreach (CLI.Model.Subject subject in subjectsController.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));

                
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor == null)
            {
                MessageBox.Show("Please choose a professor to delete!");
            }
            else
            {
                professorsController.Delete(SelectedProfessor.Id);
            }
        }
        private void UpdateProfesor_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor != null)
            {
                UpdateProfessor updateProfesor = new UpdateProfessor(professorsController,SelectedProfessor);
                updateProfesor.Show();
            }
            else
            {
                MessageBox.Show("Please choose a professor to update!");
            }

        }
    }
   
  
}
