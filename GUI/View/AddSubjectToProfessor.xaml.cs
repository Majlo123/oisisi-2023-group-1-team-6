using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using StudentskaSluzba.Serialization;
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
using CLI.Model;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddSubjectToProfessor.xaml
    /// </summary>
    public partial class AddSubjectToProfessor : Window, IObserver
    {
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public SubjectController subject_controler;
        public ProfessorSubjectController professorsubject_controler;
        public SubjectDTO SelectedSubject { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        public AddSubjectToProfessor(ProfessorDTO professor)
        {
            InitializeComponent();
            subject_controler = new SubjectController();
            professorsubject_controler = new ProfessorSubjectController();
            DataContext = this;
            subject_controler.Subscribe(this);
            professorsubject_controler.Subscribe(this);
            SelectedProfessor = professor;
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

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedSubject != null)
            {
                foreach (CLI.Model.Subject ps in professorsubject_controler.GetAllSubjectsById(SelectedProfessor.Id))
                {
                    if(SelectedSubject.SubjectId == ps.subjectId)
                    {
                        MessageBox.Show("Subject already exists!");
                        return;
                    }
                }
                professorsubject_controler.Add(SelectedProfessor.Id, SelectedSubject.subjectId);
                SelectedProfessor.ToProfessor().Subjects.Add(SelectedSubject.ToSubject());

                Close();
            }
            else
            {
                MessageBox.Show("Chose subject that you want to add.");
            }

        }
    }
}
