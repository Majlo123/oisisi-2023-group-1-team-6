using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;
using Index = StudentskaSluzba.Model.Index;

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

        public IndexController indexController { get; set; }
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
            indexController = new IndexController();
            professorsController = new ProfessorController();
            addressController = new AddressController();
            studentController = new StudentController();
            subjectsController = new SubjectController();
            addressController.Subscribe(this);
            professorsController.Subscribe(this);
            subjectsController.Subscribe(this);
            studentController.Subscribe(this);
            indexController.Subscribe(this);
            Update();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex == 0)
            {
                AddProfessor addProffessor = new AddProfessor(professorsController, addressController);
                addProffessor.Show();
            }else if(tabs.SelectedIndex == 1)
            {
                AddStudent addStudent = new AddStudent(studentController);
                addStudent.Show();
            }else if( tabs.SelectedIndex == 2)
            {
                AddSubject addSubject = new AddSubject(subjectsController);
                addSubject.Show();
            }
        }

        public void Update()
        {
            Subjects.Clear();
            Professors.Clear();
            Students.Clear();

            var addresses = addressController.GetAllAddress();
            var professors = professorsController.GetAllProfessors();

            foreach (Address address in addresses)
            {
                // Pronađi odgovarajućeg profesora za ovu adresu
                Professor professor = professors.FirstOrDefault(p => p.Address.id == address.id);

                // Ako je pronađen profesor, dodaj ga u listu
                if (professor != null)
                {
                    Professors.Add(new ProfessorDTO(professor, address));
                }
                else
                {
                    // Postupak ako ne možete pronaći odgovarajućeg profesora za adresu
                }
            }

            foreach (Student student in studentController.GetAllStudents())
            {
                Address studentAddress = addressController.getAddressById(student.Address.id);
                Index studentIndex = indexController.getIndexById(student.Index.IdIndex);
                Students.Add(new StudentDTO(student, studentAddress, studentIndex));
            }
            foreach (CLI.Model.Subject subject in subjectsController.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));

                
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex == 0)
            {
                if (SelectedProfessor == null)
                {
                    MessageBox.Show("Please choose profesor to delete!");
                }
                else
                {
                    professorsController.Delete(SelectedProfessor.Id);
                }
            }else if(tabs.SelectedIndex == 1)
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show("Please choose profesor to delete!");
                }
                else
                {
                    studentController.Delete(SelectedStudent.Id);
                }
            }else if(tabs.SelectedIndex == 2)
            {
                if (SelectedSubject == null)
                {
                    MessageBox.Show("Please choose subject to delete!");
                }
                else
                {
                    subjectsController.Delete(SelectedSubject.subjectId);
                }
            }
        }
   
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex == 0)
            {
                if (SelectedProfessor != null)
                {
                    UpdateProfessor updateProfesor = new UpdateProfessor(professorsController, SelectedProfessor, addressController);
                    updateProfesor.Show();
                }
                else
                {
                    MessageBox.Show("Please choose a professor to update!");
                }
            }else if(tabs.SelectedIndex == 2)
            {
                if (SelectedSubject != null)
                {
                    UpdateSubject updateSubject = new UpdateSubject(subjectsController, SelectedSubject);
                    updateSubject.Show();
                }
                else
                {
                    MessageBox.Show("Please choose a subject to update!");
                }
            }

        }
        
        private void About_Click(object sender, RoutedEventArgs e)
        {


            MessageBox.Show("This application is made by two students from FTN Novi Sad\n" +
                "Mihajlo Bogdanovic RA64/2021\n" +
                "Nikola Paunovic RA87/2021");



        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {


            Close();



        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {


            studentController.Save();
            professorsController.Save();
            subjectsController.Save();

            MessageBox.Show("Files are saved successfully");

        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
   
  
}
