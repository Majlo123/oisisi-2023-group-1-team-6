using CLI.Controller;
using GUI.DTO;
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
using System.Windows.Shapes;
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddProfessorToDepartment.xaml
    /// </summary>
    public partial class AddProfessorToDepartment : Window
    {
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        private ProfessorController professorController;
        private AddressController addressController;
        private DepartmentController departmentController;
        private ProfessorDepartmentController professorDepartmentController;
        public DepartmentDTO SelectedDepartment { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        private UpdateDepartment updateDepartment;

        public AddProfessorToDepartment(DepartmentDTO selectedDepartment)
        {
            InitializeComponent();
            professorController = new ProfessorController();
            addressController = new AddressController();
            departmentController = new DepartmentController();
            DataContext = this;
            professorDepartmentController = new ProfessorDepartmentController();
            SelectedDepartment = selectedDepartment;
            updateDepartment = new UpdateDepartment(departmentController, SelectedDepartment);
            Professors = new ObservableCollection<ProfessorDTO>();
            Update();
        }

        private void Update()
        {
            Professors.Clear();
            var addresses = addressController.GetAllAddress();
            var professors = professorController.GetAllProfessors();
            var prof_dep = professorDepartmentController.GetAllProfessorsById(SelectedDepartment.departmentId);

            foreach (Address address in addresses)
            {

                Professor professor = professors.FirstOrDefault(p => p.Address.id == address.id);
                if(professor != null)
                {
                    Professors.Add(new ProfessorDTO(professor));
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedProfessor != null)
            {
                professorDepartmentController.Add(SelectedProfessor.Id, SelectedDepartment.departmentId);
                Close();
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberi profesora kojeg zelis da dodas.");
                }
                else
                {
                    MessageBox.Show("Choose professor which you want to add.");
                }

            }

        }


    }
}
