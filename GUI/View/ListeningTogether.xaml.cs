using CLI.Controller;
using StudentskaSluzba.Model;
using GUI.DTO;
using CLI.Model;
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
using StudentskaSluzba.Model;
using System.Collections.ObjectModel;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ListeningTogether.xaml
    /// </summary>
    public partial class ListeningTogether : Window
    {
        
        public StudentDTO selectedStudent {  get; set; }

        public ObservableCollection<StudentDTO> Students {  get; set; }


        public ListeningTogether(List<StudentDTO> students)
        {
            InitializeComponent();          
            Students = new ObservableCollection<StudentDTO>(students);
            DataContext = this;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
