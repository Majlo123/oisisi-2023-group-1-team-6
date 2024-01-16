using CLI.Controller;
using CLI.Model;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateSubject.xaml
    /// </summary>
    public partial class UpdateSubject : Window
    {
        public int subjectid;
        public string subjectname;
        public int yearofstudy;
        string semester { get; set; }
        int espbpoints;
        public string professorid;
        public ProfessorDTO professor { get; set; }
        public SubjectDTO subject {  get; set; }
        private AddressController addressController;
        private ProfessorController professorController;
        private SubjectController subjectcontroller;
        public ObservableCollection<ProfessorDTO> professors { get; set; }
        public UpdateSubject(SubjectController subjectController,SubjectDTO selectedSubject)
        {
            InitializeComponent();
            this.subject= selectedSubject;
            subjectid = this.subject.SubjectId;
            subjectname = this.subject.SubjectName;
            yearofstudy = this.subject.YearOfStudy;
            professorid=this.subject.ProfessorId;
            if (this.subject.Semester == "Winter")
            {
                semester = "Winter";
            }
            else
            {
                semester = "Summer";
            };
            espbpoints = this.subject.EspbPoints;
            DataContext = this;
            Subject subject=new Subject(subjectid,subjectname,yearofstudy,semester,professorid,espbpoints);

            this.subject = new SubjectDTO(subject);
            subjectcontroller = subjectController;
            professorController = new ProfessorController();
            addressController = new AddressController();
            professors = new ObservableCollection<ProfessorDTO>();
            UpdateProfessors();
            
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (subject.IsValid)
            {
                subjectcontroller.Update(subject.ToSubject());
                Close();
            }
            else
            {
                MessageBox.Show("Subject can not be updated. Not all fields are valid.");
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            subjectcontroller.DeleteProfessor(subjectid);

            Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        private void GetProfesors()
        {



            professors.Clear();

            foreach (Professor professor in professorController.GetAllProfessors())
            {
                professors.Add(new ProfessorDTO(professor));
            }
        }
        private void AddProfessor_Click(object sender, RoutedEventArgs e)
        {

            AddProfessorToSubject updateProfesor = new AddProfessorToSubject(professorController, subjectcontroller, subject);
            updateProfesor.Show();
        }
        private void UpdateProfessors()
        {
            GetProfesors();

            if (professorid == null)
            {
                Add.IsEnabled = true;
                Delete.IsEnabled = false;

            }
            else
            {
                foreach (Professor professor in professorController.GetAllProfessors())
                {
                    if (professor.Id == professorid)
                    {
                        string name = professor.Name;
                        string surname = professor.Surname;
                        professor_textbox.Text = name + " " + surname;

                        Add.IsEnabled = false;
                        Delete.IsEnabled = true;

                    }

                }
            }


            professor_textbox.IsReadOnly = true;

        }
        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
