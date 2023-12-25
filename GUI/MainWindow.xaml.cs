using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IObserver
    {
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        private ProfessorController professorsController { get; set; }
        private AddressController addressController { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Professors = new ObservableCollection<ProfessorDTO>();
            professorsController = new ProfessorController();
            addressController = new AddressController();
            addressController.Subscribe(this);
            professorsController.Subscribe(this);
            Update();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddProfessor addProffessor = new AddProfessor(professorsController);
            addProffessor.Show();
        }
        
        public void Update()
        {
            Professors.Clear();
            foreach (Address address in addressController.GetAllAddress())
            foreach (Professor professor in professorsController.GetAllProfessors()) Professors.Add(new ProfessorDTO(professor,address));
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor == null)
            {
                MessageBox.Show("Please choose a professor to delete!");
            }
            else
            {
                professorsController.Delete(SelectedProfessor.Id);
            }
        }

    }
   
  
}
