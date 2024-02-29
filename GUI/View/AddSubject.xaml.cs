using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System.Windows;
using System.Windows.Controls;
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {
        public SubjectDTO Subject { get; set; }

        private SubjectController subjectsController;


        public AddSubject(SubjectController subjectsController)
        {
            InitializeComponent();
            DataContext = this;
            Subject = new SubjectDTO();
            this.subjectsController = subjectsController;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Subject.IsValid)
            {
                Subject subjectoAdd = subjectsController.getSubjectById(Subject.SubjectId);
                if (subjectoAdd != null)
                {

                    if (GlobalData.SharedString == "sr-RS")
                    {
                        MessageBox.Show("Predmet vec postoji!");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Subject already exists!");
                        return;
                    }

                }
                subjectsController.Add(Subject.ToSubject());
                Close();
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Predmet ne moze da se doda. Nisu sva polja validno popunjena.");
                }
                else
                {
                    MessageBox.Show("Subject can not be created. Not all fields are valid.");
                }

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
