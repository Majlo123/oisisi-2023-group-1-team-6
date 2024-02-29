using CLI.Controller;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using static GUI.MainWindow;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateDepartment.xaml
    /// </summary>
    public partial class UpdateDepartment : Window
    {

        public DepartmentDTO selectedDepartment;
        public ProfessorDTO professor {  get; set; }
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public ProfessorDepartment professorDepartment;
        public ProfessorDepartmentController professorDepartmentController;
        public DepartmentController departmentController;
        public AddressController addressController;

        public UpdateDepartment(DepartmentController depCon, DepartmentDTO selDep)
        {
            InitializeComponent();
            selectedDepartment = selDep;
            departmentController = depCon;
            professorDepartmentController = new ProfessorDepartmentController();
            addressController = new AddressController();
            professorDepartment = new ProfessorDepartment();
            Professors = new ObservableCollection<ProfessorDTO>();
           // professor = new ProfessorDTO();
            this.DataContext = this;
            Update();
        }

        private void Update()
        {
            Professors.Clear();
            foreach(Professor professor in professorDepartmentController.GetAllProfessorsById(selectedDepartment.departmentId))
            {
                Professors.Add(new ProfessorDTO(professor));
            }
        }

        private void AddProfessor_Closed(object sender, EventArgs e)
        {
            Update();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddProfessorToDepartment addProfessor = new AddProfessorToDepartment(selectedDepartment);
            addProfessor.Closed += AddProfessor_Closed;
            addProfessor.Show();
        }

        private void SetBoss_Click(object sender, RoutedEventArgs e)
        {
            if (professor == null)
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite profesora kojeg zelite da postavite kao sefa");
                }
                else
                {
                    MessageBox.Show("Please select professor which you want to appoint as boss.");
                }
            }
            else
            {
                string message = "";
                string title = "";
                if (GlobalData.SharedString == "sr-RS")
                {
                    message = "Da li ste sigurni da hocete da postavite ovog profesora kao sefa?";
                    title = "Postavljanje sefa katedre";
                }
                else
                {
                    message = "Are you sure that you want to appoint this professor as boss?";
                    title = "Appointing boss for department";
                }

                MessageBoxResult result =
                 MessageBox.Show(message, title,
       MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    selectedDepartment.professorId = professor.Id;
                    departmentController.Update(selectedDepartment.toDepartment());
                    Update();
                }
            }
        }

        private void DeleteProfessor_Click(Object sender, RoutedEventArgs e)
        {
            if (professor == null)
            {

                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite profesora kojeg zelite da obrisete");
                }
                else
                {
                    MessageBox.Show("Please select professor which you want to delete.");
                }
            }
            else
            {
                string message = "";
                string title = "";
                if (GlobalData.SharedString == "sr-RS")
                {
                    message = "Da li ste sigurni da hocete da obrisete profesora?";
                    title = "Brisanje profesora sa katedre";
                }
                else
                {
                    message = "Are you sure that you want to delete this professor?";
                    title = "Deleting professor from department";
                }

                MessageBoxResult result =
                 MessageBox.Show(message, title,
       MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    professorDepartmentController.Delete(selectedDepartment.departmentId);
                    Update();

                }

                else
                { }
            }

        }
    }
}
