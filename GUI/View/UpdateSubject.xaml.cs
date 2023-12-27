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

        public SubjectDTO selected {  get; set; }

        private SubjectController subjectcontroller;
        public UpdateSubject(SubjectController subjectController,SubjectDTO selectedSubject)
        {
            InitializeComponent();
            selected=selectedSubject;
            subjectid = selected.SubjectId;
            subjectname = selected.SubjectName;
            yearofstudy = selected.YearOfStudy;
            if (selected.Semester == "Winter")
            {
                semester = "Winter";
            }
            else
            {
                semester = "Summer";
            };
            espbpoints = selected.EspbPoints;
            DataContext = this;
            Subject subject=new Subject(subjectid,subjectname,yearofstudy,semester,espbpoints);

            selected = new SubjectDTO(subject);
            subjectcontroller = subjectController;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (selected.IsValid)
            {
                subjectcontroller.Update(selected.ToSubject());
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
