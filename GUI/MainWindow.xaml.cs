﻿using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
using StudentskaSluzba.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Index = StudentskaSluzba.Model.Index;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        private const int PageSize = 16;
        private int currentPageProfessor = 1;
        private int totalPageCountProfessor = 1;
        private int currentPageStudent = 1;
        private int totalPageCountStudent = 1;
        private int totalPageCountSubject = 1;
        private int curentPageSubject = 1;
        public string StatusBarString;

        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ObservableCollection<DepartmentDTO> Departments { get; set; }

        public ProfessorDTO SelectedProfessor { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        public SubjectDTO SelectedSubject { get; set; }
        public DepartmentDTO SelectedDepartment { get; set; }
        public IndexController indexController { get; set; }
        private ProfessorController professorsController { get; set; }
        private DepartmentController departmentController { get; set; }
        private SubjectController subjectsController { get; set; }
        private StudentController studentController { get; set; }
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
        public static class GlobalData
        {
            public static string SharedString = "";
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Professors = new ObservableCollection<ProfessorDTO>();
            Students = new ObservableCollection<StudentDTO>();
            Subjects = new ObservableCollection<SubjectDTO>();
            Departments = new ObservableCollection<DepartmentDTO>();
            indexController = new IndexController();
            professorsController = new ProfessorController();
            addressController = new AddressController();
            studentController = new StudentController();
            subjectsController = new SubjectController();
            departmentController = new DepartmentController();
            addressController.Subscribe(this);
            professorsController.Subscribe(this);
            subjectsController.Subscribe(this);
            studentController.Subscribe(this);
            indexController.Subscribe(this);
            departmentController.Subscribe(this);
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
            }
            else if (tabs.SelectedIndex == 1)
            {
                AddStudent addStudent = new AddStudent(studentController);
                addStudent.Show();
            }
            else if (tabs.SelectedIndex == 2)
            {
                AddSubject addSubject = new AddSubject(subjectsController);
                addSubject.Show();
            }
        }
        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
            GlobalData.SharedString = "sr-RS";
            Update();
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
            GlobalData.SharedString = "en-US";
            Update();
        }
        public void Update()
        {

            Subjects.Clear();
            Professors.Clear();
            Students.Clear();
            Departments.Clear();

            var addresses = addressController.GetAllAddress();
            var professors = professorsController.GetAllProfessors();
            var departments = departmentController.GetAllDepartments();

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
            foreach (StudentskaSluzba.Model.Subject subject in subjectsController.GetAllSubjects())
            {
                StudentskaSluzba.Model.Subject subjecttmp = subjectsController.getSubjectById(subject.subjectId);
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
                Subjects.Add(new SubjectDTO(subjecttmp));
            }

            foreach (Department department in departmentController.GetAllDepartments()) Departments.Add(new DepartmentDTO(department));
            totalPageCountProfessor = (Professors.Count + PageSize - 1) / PageSize;
            FetchEntitiesForCurrentPageProfessors();
            totalPageCountStudent = (Students.Count + PageSize - 1) / PageSize;
            FetchEntitiesForCurrentPageStudents();
            totalPageCountSubject = (Subjects.Count + PageSize - 1) / PageSize;
            FetchEntitiesForCurrentPageSubjects();
            lblDateTime.Content = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            if (GlobalData.SharedString == "sr-RS")
            {
                lblApplicationName.Content = "Studentska sluzba";


            }
            else
            {
                lblApplicationName.Content = "Student Service";
            }

            if (tabs.SelectedItem != null && tabs.SelectedItem is TabItem selectedTab)
            {
                lblTabName.Content = $"{selectedTab.Header}";
            }

        }
        private void FetchEntitiesForCurrentPageSubjects()
        {

            int startIndex = (curentPageSubject - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, Subjects.Count);


            ObservableCollection<SubjectDTO> currentEntities = new ObservableCollection<SubjectDTO>(
                Subjects.Skip(startIndex).Take(endIndex - startIndex)
            );


            SubjectsDataGrid.ItemsSource = currentEntities;
        }
        private void FetchEntitiesForCurrentPageStudents()
        {

            int startIndex = (currentPageStudent - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, Students.Count);


            ObservableCollection<StudentDTO> currentEntities = new ObservableCollection<StudentDTO>(
                Students.Skip(startIndex).Take(endIndex - startIndex)
            );


            StudentsDataGrid.ItemsSource = currentEntities;
        }
        private void FetchEntitiesForCurrentPageProfessors()
        {

            int startIndex = (currentPageProfessor - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, Professors.Count);


            ObservableCollection<ProfessorDTO> currentEntities = new ObservableCollection<ProfessorDTO>(
                Professors.Skip(startIndex).Take(endIndex - startIndex)
            );


            ProfessorsDataGrid.ItemsSource = currentEntities;
        }
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {

            if (tabs.SelectedIndex == 0)
            {
                if (currentPageProfessor > 1)
                {
                    currentPageProfessor--;
                    FetchEntitiesForCurrentPageProfessors();
                }
            }
            if (tabs.SelectedIndex == 1)
            {
                if (currentPageStudent > 1)
                {
                    currentPageStudent--;
                    FetchEntitiesForCurrentPageStudents();
                }
            }
            else if (tabs.SelectedIndex == 2)
            {
                if (curentPageSubject > 1)
                {
                    curentPageSubject--;  // Corrected
                    FetchEntitiesForCurrentPageSubjects();
                }
            }
        }

        private void ChooseTwo_Click(object sender, RoutedEventArgs e)
        {
            ChooseTwoSubjects chooseTwoSubjects = new ChooseTwoSubjects();
            chooseTwoSubjects.Show();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex == 0)
            {
                if (currentPageProfessor < totalPageCountProfessor)
                {
                    currentPageProfessor++;
                    FetchEntitiesForCurrentPageProfessors();
                }
            }
            else if (tabs.SelectedIndex == 1)
            {
                if (currentPageStudent < totalPageCountStudent)
                {
                    currentPageStudent++;
                    FetchEntitiesForCurrentPageStudents();
                }
            }
            else if (tabs.SelectedIndex == 2)
            {
                if (curentPageSubject < totalPageCountSubject)
                {
                    curentPageSubject++;
                    FetchEntitiesForCurrentPageSubjects();
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex == 0)
            {
                if (SelectedProfessor == null)
                {

                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Izaberite profesora kojeg cete da obrisete!");
                    }
                    else
                    {
                        MessageBox.Show("Please choose a professor to delete!");
                    }
                }
                else
                {
                    string messageprof = "";
                    string title = "";
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        messageprof = "Da li ste sigurni da zelite da obrisete profesora?";
                        title = "Brisanje profesora";
                    }
                    else
                    {
                        messageprof = "Are you sure that you want to delete a professor?";
                        title = "Deleting professor";
                    }
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
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Izaberite studenta kojeg cete da obrisete!");
                    }
                    else
                    {
                        MessageBox.Show("Please choose a student to delete!");
                    }
                }

                else
                {
                    string message = "";
                    string title = "";
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        message = "Da li ste sigurni da zelite da obrisete studenta?";
                        title = "Brisanje studenta";
                    }
                    else
                    {
                        message = "Are you sure that you want to delete a student?";
                        title = "Deleting student";
                    }
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
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Izaberite predmet koji cete da obrisete!");
                    }
                    else
                    {
                        MessageBox.Show("Please choose a subject to delete!");
                    }
                }
                else
                {
                    string message = "";
                    string title = "";
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        message = "Da li ste sigurni da zelite da obrisete predmet?";
                        title = "Brisanje predmeta";
                    }
                    else
                    {
                        message = "Are you sure that you want to delete a subject?";
                        title = "Deleting subject";
                    }

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
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Izaberite profesora kojeg cete da azurirate!");
                    }
                    else
                    {
                        MessageBox.Show("Please choose a professor to update!");
                    }
                }
            }
            else if (tabs.SelectedIndex == 1)
            {
                if (SelectedStudent != null)
                {
                    StudentDTO selectedStudent = GetSelectedStudent();
                    UpdateStudent updateStudent = new UpdateStudent(studentController, SelectedStudent, addressController, indexController);
                    updateStudent.Show();
                }
                else
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Izaberite studenta kojeg cete da azurirate!");
                    }
                    else
                    {
                        MessageBox.Show("Please choose a student to update!");
                    }
                }
            }
            else if (tabs.SelectedIndex == 2)
            {
                if (SelectedSubject != null)
                {

                    UpdateSubject updateSubject = new UpdateSubject(subjectsController, SelectedSubject);
                    updateSubject.Show();
                }
                else
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Izaberite predmet koji cete da azurirate!");
                    }
                    else
                    {
                        MessageBox.Show("Please choose a subject to update!");
                    }
                }
            }
            else if (tabs.SelectedIndex == 3)
            {
                if (SelectedDepartment != null)
                {
                    UpdateDepartment updateDepartment = new UpdateDepartment(departmentController, SelectedDepartment);
                    updateDepartment.Show();
                }
                else
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Izaberite katedru koju cete da azurirate!");
                    }
                    else
                    {
                        MessageBox.Show("Please choose a department to update!");
                    }
                }
            }

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {


            string message = string.Empty;
            string title = string.Empty;

            if (GlobalData.SharedString == "sr-RS")
            {
                message = "Ova aplikacija je napravljena od strane dva studenta sa FTN Novi Sad\nMihajlo Bogdanovic RA64/2021\nNikola Paunovic RA87/2021";
                title = "O aplikaciji";
            }
            else
            {
                message = "This application is made by two students from FTN Novi Sad\nMihajlo Bogdanovic RA64/2021\nNikola Paunovic RA87/2021";
                title = "About";
            }


            MessageBox.Show(message, title);



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

            if (GlobalData.SharedString == "sr-RS")
            {
                MessageBox.Show("Fajlovi su uspesno sacuvani");
            }
            else
            {
                MessageBox.Show("Files are saved");
            }

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
                    currentPageProfessor = 1;
                    FetchEntitiesForCurrentPageProfessors();
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
                else
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
                    currentPageStudent = 1;
                    FetchEntitiesForCurrentPageStudents();
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
                    else if (query.Length == 3)
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
                    curentPageSubject = 1;
                    FetchEntitiesForCurrentPageSubjects();
                    ObservableCollection<SubjectDTO> subjectTemp = new ObservableCollection<SubjectDTO>();

                    if (query.Length == 1)
                    {
                        foreach (SubjectDTO sb in Subjects)
                        {
                            if (sb.subjectName.ToLower().Contains(query[0]))
                            {
                                subjectTemp.Add(sb);
                            }
                        }
                    }
                    else if (query.Length == 2)
                    {
                        foreach (SubjectDTO sb in Subjects)
                        {
                            if (sb.subjectName.ToLower().Contains(query[0]))
                            {
                                if (sb.subjectId.ToString().Contains(query[1]))
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
                    foreach (StudentskaSluzba.Model.Subject subject in subjectsController.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));

                    SubjectsDataGrid.ItemsSource = Subjects;

                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
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
                foreach (StudentskaSluzba.Model.Subject subject in subjectsController.GetAllSubjects())
                {
                    StudentskaSluzba.Model.Subject subjecttmp = subjectsController.getSubjectById(subject.subjectId);
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
                    Subjects.Add(new SubjectDTO(subjecttmp));
                }
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
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.U))
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
                tabs.SelectedIndex = 1;
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.F3))
                tabs.SelectedIndex = 2;
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                MenuItem_Click_English(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
                MenuItem_Click_Serbian(sender, e);

        }
    }


}
