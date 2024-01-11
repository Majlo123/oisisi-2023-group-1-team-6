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
    /// Interaction logic for TakeExam.xaml
    /// </summary>
    public partial class TakeExam : Window
    {
        public int subjectid;
        public string subjectname;
        
        public SubjectDTO Subject {  get; set; }
        
        public TakeExam(SubjectDTO selectedsubject)
        {
            InitializeComponent();
            this.Subject = selectedsubject;
            subjectid = this.Subject.subjectId;
            subjectname = this.Subject.subjectName;
            DataContext = this;
            Subject subject=new Subject(subjectid, subjectname);
            this.Subject = new SubjectDTO(subject);

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
