using CLI.Controller;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using Subject = StudentskaSluzba.Model.Subject;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ChooseTwoSubjects.xaml
    /// </summary>
    public partial class ChooseTwoSubjects : Window
    {
        public int subject1;
        public int subject2;
        public SubjectDTO Subject1 { get; set; }
        public SubjectDTO Subject2 { get; set; }
        public SubjectDTO selectedSubject1 {  get; set; }
        public SubjectDTO selectedSubject2 { get; set; }
        public SubjectController subjectController;
        public IndexController indexController;
        public AddressController addressController;
        public StudentSubjectController studentSubjectController;
        public GradeController gradeController;
        public ChooseTwoSubjects()
        {
            InitializeComponent();
            subjectController = new SubjectController();
            selectedSubject1 = new SubjectDTO();
            selectedSubject2 = new SubjectDTO();
            indexController = new IndexController();
            addressController = new AddressController();
            studentSubjectController = new StudentSubjectController();
            gradeController = new GradeController();
        }

        public void UpdateProfessorTextboxOne()
        {


            if (subject1 == 0)
            {
                DeleteOne.IsEnabled = false;
                AddOne.IsEnabled = true;
            }

            else
            {
                foreach (StudentskaSluzba.Model.Subject subject in subjectController.GetAllSubjects())
                {
                    if (subject.subjectId == subject1)
                    {
                        string name = subject.subjectName;
                        subjectNumberOne_textbox.Text = name;


                        DeleteOne.IsEnabled = true;
                        AddOne.IsEnabled = false;

                    }
                    else if (subjectNumberOne_textbox.Text == "")
                    {
                        AddOne.IsEnabled = true;
                        DeleteOne.IsEnabled = false;


                    }

                }
            }
            subjectNumberOne_textbox.IsReadOnly = true;
            subjectNumberOne_textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }
        public void ClearProfessorTextboxOne()
        {
            subject1 = 0;

            DeleteOne.IsEnabled = false;
            AddOne.IsEnabled = true;
            subjectNumberOne_textbox.Text = "";

            subjectNumberOne_textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        public void UpdateProfessorTextboxTwo()
        {


            if (subject2 == 0)
            {
                DeleteTwo.IsEnabled = false;
                AddTwo.IsEnabled = true;
            }

            else
            {
                foreach (StudentskaSluzba.Model.Subject subject in subjectController.GetAllSubjects())
                {
                    if (subject.subjectId == subject2)
                    {
                        string name = subject.subjectName;
                        subjectNumberTwo_textbox.Text = name;


                        DeleteTwo.IsEnabled = true;
                        AddTwo.IsEnabled = false;

                    }
                    else if (subjectNumberTwo_textbox.Text == "")
                    {
                        AddTwo.IsEnabled = true;
                        DeleteTwo.IsEnabled = false;


                    }

                }
            }
            subjectNumberTwo_textbox.IsReadOnly = true;
            subjectNumberTwo_textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }
        public void ClearProfessorTextboxTwo()
        {
            subject2 = 0;

            DeleteTwo.IsEnabled = false;
            AddTwo.IsEnabled = true;
            subjectNumberTwo_textbox.Text = "";

            subjectNumberTwo_textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        private void AddSubjectOne_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectToChooseTwo1 addSubject = new AddSubjectToChooseTwo1(this);
            addSubject.Show();
        }

        private void AddSubjectTwo_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectToChooseTwo2 addSubject = new AddSubjectToChooseTwo2(this);
            addSubject.Show();
        }

        private void ListeningTogetherButton_Click(object sender, RoutedEventArgs e)
        {
            List<StudentDTO> Students = new List<StudentDTO>();

            Subject subb = subjectController.GetSubjectByName(subjectNumberOne_textbox.Text);
            Subject1 = new SubjectDTO(subb);
            Subject subb2 = subjectController.GetSubjectByName(subjectNumberTwo_textbox.Text);
            Subject2 = new SubjectDTO(subb2);

            var studentsFirstSubject = studentSubjectController.getAllStudentsBySubjectId(Subject1.SubjectId);
            var studentsSecondSubject = studentSubjectController.getAllStudentsBySubjectId(Subject2.SubjectId);

            foreach (Student prvi in studentsFirstSubject)
            {
                foreach (Student drugi in studentsSecondSubject)
                {
                    if (prvi.Id == drugi.Id)
                    {
                        Address studentAddress = addressController.getAddressById(prvi.Address.id);
                        StudentskaSluzba.Model.Index studentIndex = indexController.getIndexById(prvi.Index.IdIndex);
                        Students.Add(new StudentDTO(prvi, studentAddress, studentIndex));
                    }
                }
            }

            ListeningTogether listeningTogether = new ListeningTogether(Students);
            listeningTogether.Show();
        }

        private void PassedFailedButton_Click(object sender, RoutedEventArgs e)
        {
            List<StudentDTO> Students = new List<StudentDTO>();

            Subject subb = subjectController.GetSubjectByName(subjectNumberOne_textbox.Text);
            Subject1 = new SubjectDTO(subb);
            Subject subb2 = subjectController.GetSubjectByName(subjectNumberTwo_textbox.Text);
            Subject2 = new SubjectDTO(subb2);

            var studentsFirstSubject = studentSubjectController.getAllStudentsBySubjectId(Subject1.SubjectId);
            var studentsFirstSubjectGraded = gradeController.getStudentsBySubjectId(Subject1.SubjectId);
            var studentsSecondSubject = studentSubjectController.getAllStudentsBySubjectId(Subject2.SubjectId);
            var studentsSecondSubjectGraded = gradeController.getStudentsBySubjectId(Subject2.SubjectId);

            var firstExcept = studentsFirstSubjectGraded.Except(studentsSecondSubject);
            var secondExcept = studentsSecondSubjectGraded.Except(studentsFirstSubject);

            foreach(Student stud in firstExcept)
            {
                Address studentAddress = addressController.getAddressById(stud.Address.id);
                StudentskaSluzba.Model.Index studentIndex = indexController.getIndexById(stud.Index.IdIndex);
                Students.Add(new StudentDTO(stud, studentAddress, studentIndex));
            }

            foreach(Student stud in secondExcept)
            {
                Address studentAddress = addressController.getAddressById(stud.Address.id);
                StudentskaSluzba.Model.Index studentIndex = indexController.getIndexById(stud.Index.IdIndex);
                Students.Add(new StudentDTO(stud, studentAddress, studentIndex));
            }

            PassedFailed passedFailed = new PassedFailed(Students);
            passedFailed.Show();


        }

        private void DeleteSubjectOne_Click(object sender, RoutedEventArgs e)
        {
            subjectNumberOne_textbox.Text = "";
        }

        private void DeleteSubjectTwo_Click(object sender, RoutedEventArgs e)
        {
            subjectNumberTwo_textbox.Text = "";
        }
    }
}
