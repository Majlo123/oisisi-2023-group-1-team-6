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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for TakeExam.xaml
    /// </summary>
    public partial class TakeExam : Window
    {
        
        private GradeController gradecontroller;
        public ObservableCollection<CLI.Model.Subject> Subjects { get; set; }
        public SubjectDTO Subject {  get; set; }
        public StudentSubjectController studentSubjectController;
        public StudentDTO Student { get; set; }
        public GradeDTO Grade { get; set; }
        public DateTime? dateTime { get; set; }
        public DateOnly Date;
        public TakeExam(SubjectDTO selectedsubject,StudentDTO selectedstudent)
        {
            InitializeComponent();
            
            
            DataContext = this;
            Grade = new GradeDTO();
            
            gradecontroller = new GradeController();
            Subject = selectedsubject;
            Student = selectedstudent;
            

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            
            switch (Grade.Grade)
            {
                case "10":
                    Grade.Grade= "10";
                    break;
                case "9":
                    Grade.Grade = "9";
                    break;
                case "8":
                    Grade.Grade = "8";
                    break;
                case "7":
                    Grade.Grade = "7";
                    break;
                case "6":
                    Grade.Grade = "6";
                    break;
            }
            Grade.Date = new DateOnly(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
            Grade.Student_Id = Student.Id;
            Grade.Subject_Id = Subject.subjectId;
            gradecontroller.Add(Grade.ToGrade());
            StudentSubjectController ssc= new StudentSubjectController();
            ssc.Delete(Subject.SubjectId);
            Close();
        }
    }
}
