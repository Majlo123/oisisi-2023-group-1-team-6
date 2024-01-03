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
        public SubjectDTO Subject { get; set; }

        private SubjectController subjectsController { get; set; }

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public AddSubjectToStudent(SubjectController subjectController)
        {
            InitializeComponent();
            DataContext = this;
            Subjects = new ObservableCollection<SubjectDTO>();
            Subject = new SubjectDTO();
            subjectsController = new SubjectController();
            subjectsController.Subscribe(this);
            this.subjectsController = subjectsController;
            Update();
        }


        public void Update()
        {
            Subjects.Clear();

            foreach (CLI.Model.Subject subject in subjectsController.GetAllSubjects()) Subjects.Add(new SubjectDTO(subject));



        }
        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            // Check if Subject is null
            if (Subject == null)
            {
                MessageBox.Show("Please choose a subject to add!");
                return; // Exit the method early as Subject is not selected
            }

            
                // Assuming SubjectId is a property within SubjectDTO
                CLI.Model.Subject subjectToAdd = subjectsController.getSubjectById(Subject.subjectId);

                if (subjectToAdd != null)
                {
                    subjectsController.Add(subjectToAdd);
                    MessageBox.Show(subjectToAdd.ToString());
                    Close();
                }
                else
                {
                    MessageBox.Show("The selected subject does not exist.");
                }
            }
        }
    }





