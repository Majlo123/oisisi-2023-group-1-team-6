using CLI.Controller;
using CLI.Observer;
using CLI.Model;
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
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        public string StatusBarString;

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
            app = (App)Application.Current;
            app.ChangeLanguage(ENG);
            
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
        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
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
                    string messageprof = "Are you sure that you want to delete a professor?";
                    string title = "Deleting professor";

                    MessageBoxResult result =
                     MessageBox.Show(messageprof, title,
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
            if (tabs.SelectedIndex == 1)
            {
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    string writentext = SearchText.ToLower();
                    writentext = writentext.Replace(" ", String.Empty);
                    string[] query = writentext.Split(',');

                    ObservableCollection<StudentDTO> studentTemp = new ObservableCollection<StudentDTO>();

                    if (query.Length == 1)
                    {
                        foreach (StudentDTO st in Students)
                        {
                            if (st.surname.ToLower().Contains(query[0]))
                            {
                                studentTemp.Add(st);
                            }
                        }
                    }
                    else if (query.Length == 2)
                    {
                        foreach (StudentDTO st in Students)
                        {
                            if (st.surname.ToLower().Contains(query[0]))
                            {
                                if (st.name.ToLower().Contains(query[1]))
                                {
                                    studentTemp.Add(st);
                                }
                            }
                        }
                    }
                    else if(query.Length == 3)
                    {
                        foreach (StudentDTO st in Students)
                        {
                            if (st.printIndex.ToLower().Contains(query[0]))
                            {
                                if (st.name.ToLower().Contains(query[1]))
                                {
                                    if (st.surname.ToLower().Contains(query[2]))
                                        studentTemp.Add(st);
                                }
                            }
                        }
                    }
                    Students = studentTemp;
                    StudentsDataGrid.ItemsSource = Students;

                }
                else
                {
                    Students.Clear();
                    var addresses = addressController.GetAllAddress();
                    var students = studentController.GetAllStudents();
                    var indexes = indexController.GetAllIndexes();

                    foreach (Student student in students)
                    {
                        Address studentAddress = addressController.getAddressById(student.Address.id);
                        Index studentIndex = indexController.getIndexById(student.Index.IdIndex);
                        Students.Add(new StudentDTO(student, studentAddress, studentIndex));
                    }

                    

                    StudentsDataGrid.ItemsSource = Students;

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
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                Add_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                Delete_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                Update_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S))
                Save_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.C))
                Close_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A))
                About_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.F1))
                tabs.SelectedIndex = 0;
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.F2))
                tabs.SelectedIndex=1;
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.F3))
                tabs.SelectedIndex=2;
            
        }
    }
   
  
}
