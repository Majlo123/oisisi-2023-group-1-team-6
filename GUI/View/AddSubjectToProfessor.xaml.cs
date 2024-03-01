using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static GUI.MainWindow;

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
        public UpdateProfessor updateProfessor;
        public ProfessorController professorController;
        public AddressController addressController;
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
            professorController = new ProfessorController();
            addressController = new AddressController();
            updateProfessor = new UpdateProfessor(professorController, SelectedProfessor, addressController);
            Subjects = new ObservableCollection<SubjectDTO>();
            Update();
        }

        public void Update()
        {
            Subjects.Clear();

            foreach (StudentskaSluzba.Model.Subject subject in subject_controler.GetAllSubjects())
            {
                SubjectDTO subject1 = updateProfessor.Subjects.FirstOrDefault(s => s.SubjectId == subject.subjectId);

                if (subject1 == null)
                {
                    Subjects.Add(new SubjectDTO(subject));
                }
            }



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
                foreach (StudentskaSluzba.Model.Subject ps in professorsubject_controler.GetAllSubjectsById(SelectedProfessor.Id))
                {
                    if (SelectedSubject.SubjectId == ps.subjectId)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            MessageBox.Show("Predmet vec postoji!");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Subject already exists!");
                            return;
                        }

                    }
                }
                professorsubject_controler.Add(SelectedProfessor.Id, SelectedSubject.subjectId);

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
