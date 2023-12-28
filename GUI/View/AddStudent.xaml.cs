using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
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
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public StudentDTO Student { get; set; }

        private StudentController studentController;

        public AddStudent(StudentController studentController)
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            this.studentController = studentController;

        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Student.IsValid)
            {
                Student subjectoAdd = studentController.getStudentById(Student.Id);
                if (subjectoAdd != null)
                {
                    MessageBox.Show("Student with this id already exist.");
                    return;

                }
                studentController.Add(Student.toStudent());
                Close();
            }
            else
            {
                MessageBox.Show("Student can not be created. Not all fields are valid.");
            }
        }

            private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        
    }
}
