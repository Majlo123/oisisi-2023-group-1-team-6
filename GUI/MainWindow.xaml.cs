using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
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

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;

                OnPropertyChanged("SearchText");
            }
        }
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
               
                Professor professor = professors.FirstOrDefault(p => p.Address.id == address.id);

                if (professor != null)
                {
                    Professors.Add(new ProfessorDTO(professor, address));
                }
                
            }

            foreach (Student student in studentController.GetAllStudents())
            {
                Address studentAddress = addressController.getAddressById(student.Address.id);
                Index studentIndex = indexController.getIndexById(student.Index.IdIndex);
                Students.Add(new StudentDTO(student, studentAddress, studentIndex));
            }
            foreach (CLI.Model.Subject subject in subjectsController.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));

            lblDateTime.Content = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            lblApplicationName.Content = "Studentska sluzba";

            if (tabs.SelectedItem != null && tabs.SelectedItem is TabItem selectedTab)
            {
                lblTabName.Content = $"{selectedTab.Header}";
            }

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
                    string message = "Are you sure that you want to delete a professor?";
                    string title = "Deleting professor";

                    MessageBoxResult result =
                     MessageBox.Show(message, title,
           MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        
                        professorsController.Delete(SelectedProfessor.Id);
                    }

                    else
                    { }

                }
            }
            else if (tabs.SelectedIndex == 1)
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show("Please choose student to delete!");
                }
               
                   else
                    {
                        string message = "Are you sure that you want to delete a student?";
                        string title = "Deleting student";

                        MessageBoxResult result =
                         MessageBox.Show(message, title,
               MessageBoxButton.OKCancel);

                        if (result == MessageBoxResult.OK)
                        {
                           studentController.Delete(SelectedStudent.Id);
                        }

                        else
                        { }
                    }
                }
            
            else if (tabs.SelectedIndex == 2)
            {
                if (SelectedSubject == null)
                {
                    MessageBox.Show("Please choose subject to delete!");
                }
                else
                {
                    string message = "Are you sure that you want to delete a subject?";
                    string title = "Deleting subject";

                    MessageBoxResult result =
                     MessageBox.Show(message, title,
           MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        subjectsController.Delete(SelectedSubject.SubjectId);
                    }

                    else
                    { }
                }
            }
        }

        public StudentDTO GetSelectedStudent()
        {
            if (StudentsDataGrid.SelectedItem != null && StudentsDataGrid.SelectedItem is StudentDTO selectedStudent)
            {
                return selectedStudent;
            }
            return null;
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
            }
            else if(tabs.SelectedIndex == 1)
            {
                if (SelectedStudent != null)
                {
                    StudentDTO selectedStudent = GetSelectedStudent();
                    UpdateStudent updateStudent = new UpdateStudent(studentController, SelectedStudent, addressController,indexController);
                    updateStudent.Show();
                }
                else
                {
                    MessageBox.Show("Please choose a student to update!");
                }
            }
            else if(tabs.SelectedIndex == 2)
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
        public void Search_Click(object sender, RoutedEventArgs e)
        {

            if (tabs.SelectedIndex == 0)
            {
                if (SearchText != "")
                {
                    string writentext = SearchText.ToLower();
                    writentext = writentext.Replace(" ", String.Empty);
                    string[] query = writentext.Split(',');

                    ObservableCollection<ProfessorDTO> professorTemp = new ObservableCollection<ProfessorDTO>();

                    if (query.Length == 1)
                    {
                        foreach (ProfessorDTO p in Professors)
                        {
                            if (p.Surname.ToLower().Contains(query[0]))
                            {
                                professorTemp.Add(p);
                            }
                        }
                    }
                    else if (query.Length == 2)
                    {
                        foreach (ProfessorDTO p in Professors)
                        {
                            if (p.Surname.ToLower().Contains(query[0]))
                            {
                                if (p.Name.ToLower().Contains(query[1]))
                                {
                                    professorTemp.Add(p);
                                }
                            }
                        }
                    }
                    Professors = professorTemp;
                    ProfessorsDataGrid.ItemsSource = Professors;

                }
                else {
                    Professors.Clear();
                    var addresses = addressController.GetAllAddress();
                    var professors = professorsController.GetAllProfessors();
                    foreach (Address address in addresses)
                    {

                        Professor professor = professors.FirstOrDefault(p => p.Address.id == address.id);

                        if (professor != null)
                        {
                            Professors.Add(new ProfessorDTO(professor, address));
                        }

                    }
                    
                    ProfessorsDataGrid.ItemsSource = Professors;
                    
                }
            }
            if (tabs.SelectedIndex == 2)
            {
                if (SearchText != "")
                {
                    string writentext = SearchText.ToLower();
                    writentext = writentext.Replace(" ", String.Empty);
                    string[] query = writentext.Split(',');

                    ObservableCollection<SubjectDTO> subjectTemp = new ObservableCollection<SubjectDTO>();

                    if (query.Length == 1)
                    {
                        foreach (SubjectDTO sb in Subjects)
                        {
                            if (sb.subjectId.ToString().Contains(query[0]))
                            {
                                subjectTemp.Add(sb);
                            }
                        }
                    }
                    else if (query.Length == 2)
                    {
                        foreach (SubjectDTO sb in Subjects)
                        {
                            if (sb.subjectId.ToString().Contains(query[0]))
                            {
                                if (sb.subjectName.ToLower().Contains(query[1]))
                                {
                                    subjectTemp.Add(sb);
                                }
                            }
                        }
                    }
                    Subjects = subjectTemp;
                    SubjectsDataGrid.ItemsSource = Subjects;

                }
                else
                {
                    Subjects.Clear();
                    foreach (CLI.Model.Subject subject in subjectsController.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));

                    SubjectsDataGrid.ItemsSource = Subjects;

                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e) {
            
            if (tabs.SelectedIndex == 0)
            {
                Professors.Clear();
                var addresses = addressController.GetAllAddress();
                var professors = professorsController.GetAllProfessors();

                foreach (Address address in addresses)
                {

                    Professor professor = professors.FirstOrDefault(p => p.Address.id == address.id);

                    if (professor != null)
                    {
                        Professors.Add(new ProfessorDTO(professor, address));
                    }

                }
            }
            else if (tabs.SelectedIndex == 1)
            {
                Students.Clear();
                foreach (Student student in studentController.GetAllStudents())
                {
                    Address studentAddress = addressController.getAddressById(student.Address.id);
                    Index studentIndex = indexController.getIndexById(student.Index.IdIndex);
                    Students.Add(new StudentDTO(student, studentAddress, studentIndex));
                }
            }
            else if (tabs.SelectedIndex == 2)
            {
                Subjects.Clear();
                foreach (CLI.Model.Subject subject in subjectsController.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
   
  
}
