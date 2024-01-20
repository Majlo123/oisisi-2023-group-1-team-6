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
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public StudentDTO Student { get; set; }

        private StudentController studentController;

        public DateTime? dateTime { get; set; }
        public DateOnly Date;

        public AddStudent(StudentController studentController)
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            this.studentController = studentController;
            dateTime = null;
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Student.IsValid && dateTime != null)
            {
                Student studentToAdd = studentController.getStudentById(Student.Id);
                if (studentToAdd != null)
                {
                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Student vec postoji!");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Student already exists!");
                        return;
                    }

                }
                
                Student.date = new DateOnly(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);

                
                studentController.Add(Student.toStudent());
                Close();
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Student ne moze da se doda. Nisu sva polja validno popunjena.");
                }
                else
                {
                    MessageBox.Show("Student can not be created. Not all fields are valid.");
                }
            }
        }

            private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        
    }
}
