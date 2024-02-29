using GUI.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ShowProfessorsWindow.xaml
    /// </summary>
    public partial class ShowProfessorsWindow : Window
    {
        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        public ProfessorDTO SelectedProfessor { get; set; }
        public ShowProfessorsWindow(List<ProfessorDTO> professors)
        {
            InitializeComponent();
            Professors = new ObservableCollection<ProfessorDTO>(professors);
            DataContext = this;
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
