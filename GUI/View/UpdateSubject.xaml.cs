using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for UpdateSubject.xaml
    /// </summary>
    public partial class UpdateSubject : Window
    {
        public int subjectid;
        public string subjectname;
        public int yearofstudy;
        string semester { get; set; }
        int espbpoints;

        public SubjectDTO subject {  get; set; }

        private SubjectController subjectcontroller;
        public UpdateSubject(SubjectController subjectController,SubjectDTO selectedSubject)
        {
            InitializeComponent();
            this.subject= selectedSubject;
            subjectid = this.subject.SubjectId;
            subjectname = this.subject.SubjectName;
            yearofstudy = this.subject.YearOfStudy;
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
            Subject subject=new Subject(subjectid,subjectname,yearofstudy,semester,espbpoints);

            this.subject = new SubjectDTO(subject);
            subjectcontroller = subjectController;
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
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
