using GUI.DTO;
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
    /// Interaction logic for PassedFailed.xaml
    /// </summary>
    public partial class PassedFailed : Window
    {
        public StudentDTO selectedStudent { get; set; }

        public ObservableCollection<StudentDTO> Students { get; set; }


        public PassedFailed(List<StudentDTO> students)
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
