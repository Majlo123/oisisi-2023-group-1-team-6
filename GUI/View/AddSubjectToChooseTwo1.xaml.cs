using CLI.Controller;
using StudentskaSluzba.Model;
using GUI.DTO;
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
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddSubjectToChooseTwo1.xaml
    /// </summary>
    public partial class AddSubjectToChooseTwo1 : Window
    {
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public SubjectDTO selectedSubject { get; set; }
        public ChooseTwoSubjects chooseTwo;
        public SubjectController subjectController;
        public AddSubjectToChooseTwo1(ChooseTwoSubjects ch)
        {
            InitializeComponent();
            subjectController = new SubjectController();
            Subjects = new ObservableCollection<SubjectDTO>();
            selectedSubject = new SubjectDTO();
            chooseTwo = ch;
            this.DataContext = this;
            Update();
        }

        private void Update()
        {
            Subjects.Clear();
            foreach(Subject subject in subjectController.GetAllSubjects())
            {
                 Subjects.Add(new SubjectDTO(subject));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            if(selectedSubject == null)
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberi predmet koji zelis da dodas.");
                }
                else
                {
                    MessageBox.Show("Choose subject that you want to add.");
                }
            }
            else
            {
                chooseTwo.subjectNumberOne_textbox.Text = selectedSubject.subjectName;
                Close();
            }
        }
    }
}
