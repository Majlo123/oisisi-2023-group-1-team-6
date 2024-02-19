using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using static GUI.MainWindow;

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
        public SubjectController subjectcontroller;
        public ObservableCollection<ProfessorDTO> professors { get; set; }
        public UpdateSubject(SubjectController subjectController,SubjectDTO selectedSubject)
        {
            InitializeComponent();
            this.subject= selectedSubject;
            subjectid = this.subject.SubjectId;
            subjectname = this.subject.SubjectName;
            yearofstudy = this.subject.YearOfStudy;
            
            professorid =this.subject.Professor;
            
            if (this.subject.Semester == "Winter")
            {

                if (GlobalData.SharedString == "sr-RS")
                {
                    semester = "Zimski";
                }
                else
                {
                    semester = "Winter";
                }
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    semester = "Letnji";
                }
                else
                {
                    semester = "Summer";
                }
            };
            espbpoints = this.subject.EspbPoints;
            
            Subject subject=new Subject(subjectid,subjectname,yearofstudy,semester,professorid,espbpoints);

            this.subject = new SubjectDTO(subject);
            subjectcontroller = subjectController;
            professorController = new ProfessorController();
            addressController = new AddressController();

            UpdateProfessorTextbox(professorid);
            DataContext = this;
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
                
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Predmet ne moze da se azurira. Nisu sva polja validno populjena");
                }
                else
                {
                    MessageBox.Show("Subject can not be updated. Not all fields are valid.");
                }
                
            }
        }
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            string title = "";
            if (GlobalData.SharedString == "sr-RS")
            {
                message = "Da li ste sigurni da zelite da obrisete profesora sa predmeta?";
                title = "Brisanje profesora sa predmeta";
            }
            else
            {
                message = "Are you sure that you want to delete a professor from subject?";
                title = "Deleting professor from subject";
            }

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                subject.Professor = "";
                subjectcontroller.DeleteProfessor(subjectid);
                ClearProfessorTextbox();  
                
            }
            else { }
        }

        private void GetProfesors()
        {



            professors = new ObservableCollection<ProfessorDTO>();

            foreach (Professor professor in professorController.GetAllProfessors())
                {
                    professors.Add(new ProfessorDTO(professor));
                }
            
        }
        public void UpdateProfessorTextbox(string professorId)
        {


            if (professorid == "")
            {
                Delete.IsEnabled = false;
                Add.IsEnabled = true;
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


                        Delete.IsEnabled = true;
                        Add.IsEnabled = false;

                    }
                    else if (professor_textbox.Text == "")
                    {
                        Add.IsEnabled = true;
                        Delete.IsEnabled = false;


                    }

                }
            }
            professor_textbox.IsReadOnly = true;
            professor_textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }
        public void ClearProfessorTextbox()
        {
            professorid = "";

            Delete.IsEnabled = false;
            Add.IsEnabled = true;
            professor_textbox.Text = ""; 

            professor_textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        private void AddProfessor_Click(object sender, RoutedEventArgs e)
        {
            AddProfessorToSubject updateProfesor = new AddProfessorToSubject(professorController, subjectcontroller, subject,this);

            updateProfesor.Show();
        }
        private void AddSubject_Closed(object sender, EventArgs e)
        {
            UpdateProfessors();
        }
        public void UpdateProfessors()
        {
            GetProfesors();

            if (professorid == "")
            {
                Delete.IsEnabled = false;
                Add.IsEnabled = true;
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

                        professor_textbox.IsReadOnly = true;
                    }
                }
            }

           
            professor_textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }
    }
    
}
