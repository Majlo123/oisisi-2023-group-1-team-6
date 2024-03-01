using CLI.Controller;
using GUI.DTO;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for TakeExam.xaml
    /// </summary>
    public partial class TakeExam : Window
    {

        private GradeController gradecontroller;
        public ObservableCollection<StudentskaSluzba.Model.Subject> Subjects { get; set; }
        public SubjectDTO Subject { get; set; }
        public StudentSubjectController studentSubjectController;
        public StudentDTO Student { get; set; }
        public GradeDTO Grade { get; set; }
        public DateTime? dateTime { get; set; }
        public DateOnly Date;
        public TakeExam(SubjectDTO selectedsubject, StudentDTO selectedstudent, StudentSubjectController ssc)
        {
            InitializeComponent();


            DataContext = this;
            Grade = new GradeDTO();

            gradecontroller = new GradeController();
            Subject = selectedsubject;
            Student = selectedstudent;
            studentSubjectController = ssc;


        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {

            switch (Grade.Grade)
            {
                case 10:
                    Grade.Grade = 10;
                    break;
                case 9:
                    Grade.Grade = 9;
                    break;
                case 8:
                    Grade.Grade = 8;
                    break;
                case 7:
                    Grade.Grade = 7;
                    break;
                case 6:
                    Grade.Grade = 6;
                    break;
            }
            if (dateTime.HasValue)
            {
                Date = new DateOnly(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);

            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Niste popunili sve podatke za polaganje ispita");
                    return;
                }
                else
                {
                    MessageBox.Show("You have not filled in all the data for taking the exam");
                    return;
                }
            }
            if (Grade.Grade == 0)
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Niste popunili sve podatke za polaganje ispita");
                    return;
                }
                else
                {
                    MessageBox.Show("You have not filled in all the data for taking the exam");
                    return;
                }
            }
            Grade.date = Date;
            Grade.StudentId = Student.Id;
            Grade.SubjectId = Subject.subjectId;
            gradecontroller.Add(Grade.ToGrade());
            studentSubjectController.Delete(Grade.studentId);

            Close();
        }
    }
}
