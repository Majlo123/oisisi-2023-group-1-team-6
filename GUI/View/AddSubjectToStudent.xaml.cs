using CLI.Controller;
using CLI.Observer;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddSubjectToStudent.xaml
    /// </summary>
    public partial class AddSubjectToStudent : Window, IObserver
    {


        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public SubjectController subject_controler;
        public StudentSubjectController studentsubject_controler;
        public SubjectDTO SelectedSubject { get; set; }
        public StudentDTO selectedStudent { get; set; }
        public AddSubjectToStudent(StudentDTO student)
        {
            InitializeComponent();
            subject_controler = new SubjectController();
            studentsubject_controler = new StudentSubjectController();
            DataContext = this;
            subject_controler.Subscribe(this);
            studentsubject_controler.Subscribe(this);
            selectedStudent = student;
            Subjects = new ObservableCollection<SubjectDTO>();
            Update();
        }


        public void Update()
        {
            Subjects.Clear();

            foreach (CLI.Model.Subject subject in subject_controler.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));



        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedSubject != null)
            {
                studentsubject_controler.Add(selectedStudent.id, SelectedSubject.subjectId);
                
                Close();
            }
            else
            {
                MessageBox.Show("Chose subject that you want to add.");
            }

        }
    }
}
    

            
        
    





