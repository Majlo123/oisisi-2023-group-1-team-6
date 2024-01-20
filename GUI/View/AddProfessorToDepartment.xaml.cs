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
        public DepartmentDTO SelectedDepartment { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }

        public UpdateDepartment updateDepartment;
        public AddProfessorToDepartment(ProfessorController ps, DepartmentController dc, DepartmentDTO selectedDepartment, UpdateDepartment updateDepartment)
        {
            InitializeComponent();
            this.professorController = ps;
            addressController = new AddressController();
            this.departmentController = dc;
            this.SelectedDepartment = selectedDepartment;
            this.updateDepartment = updateDepartment;
            Professors = new ObservableCollection<ProfessorDTO>();
            DataContext = this;
            Update();
        }

        private void Update()
        {

            updateDepartment.Professors.Clear();
            departmentController.Update(SelectedDepartment.toDepartment());

            var addresses = addressController.GetAllAddress();
            var professors = professorController.GetAllProfessors();

            foreach (Address address in addresses)
            {

                Professor professor = professors.FirstOrDefault(p => p.Address.id == address.id);

                if (professor != null)
                {
                    Professors.Add(new ProfessorDTO(professor, address));
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
                professorController.Add(SelectedProfessor.ToProfessor());
                SelectedDepartment.toDepartment().Professors.Add(SelectedProfessor.ToProfessor());
                departmentController.Update(SelectedDepartment.toDepartment());

                updateDepartment.Update();
                Close();
            }
            else
            {
                MessageBox.Show("Chose professor that you want to add.");
            }
        }

        public void AddBoss_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor != null)
            {
                string id_prof = SelectedProfessor.Id;
                SelectedDepartment.professorId = id_prof;
                departmentController.Update(SelectedDepartment.toDepartment());

                // Ažuriranje professorid u UpdateSubject klasi
                updateDepartment.professorId = id_prof;

                Close();
            }
            else
            {
                MessageBox.Show("SelectedProfessor is null. Please choose a professor.");
            }
        }

    }
}
