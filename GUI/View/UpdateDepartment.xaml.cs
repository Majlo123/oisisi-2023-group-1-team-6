using CLI.Controller;
using CLI.Model;
using CLI.Observer;
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
    /// Interaction logic for UpdateDepartment.xaml
    /// </summary>
    public partial class UpdateDepartment : Window
    {

        public string name;
        public string surname;
        public string email;
        public string title;
        public string professorId;

        public ProfessorDepartment professorDepartment;

        public ProfessorDepartmentController professorDepartmentController;
        public ProfessorDTO professor { get; set; }
        public DepartmentDTO department { get; set; }
        public List<ProfessorDTO> professors { get; set; }

        private ProfessorController professorController;

        private DepartmentController departmentController;

        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public UpdateDepartment(DepartmentController dc, DepartmentDTO SelectedDepartment)
        {
            InitializeComponent();
            this.department = SelectedDepartment;
            professorController = new ProfessorController();
            this.departmentController = dc;
            Professors = new ObservableCollection<ProfessorDTO>();
            professorDepartmentController = new ProfessorDepartmentController();
            professorDepartmentController.GetAllProfessorsById(department.departmentId)
            .Select(prof => new ProfessorDTO(prof))
            .ToList();
            DataContext = this;
            Update();
        }

        private void Update()
        {

            Professors.Clear();
            foreach (Professor subject in professorDepartmentController.GetAllProfessorsById(department.departmentId))
                Professors.Add(new ProfessorDTO(subject));


        }
        private void AddProfessor_Closed(object sender, EventArgs e)
        {
            Update();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddProfessor_Click(object sender, EventArgs e)
        {
            AddProfessorToDepartment addProfessor = new AddProfessorToDepartment(department,professor);
            addProfessor.Closed += AddProfessor_Closed;
            addProfessor.Show();
        }



        private void GetProfesors()
        {
            Professors = new ObservableCollection<ProfessorDTO>();

            foreach (Professor professor in professorController.GetAllProfessors())
            {
                Professors.Add(new ProfessorDTO(professor));
            }

        }

        private void SetBoss_Click(object sender, RoutedEventArgs e)
        {
            AddProfessorToDepartment updateProfesor = new AddProfessorToDepartment(department,professor);

            updateProfesor.AddBoss_Click(sender, e);
            updateProfesor.Closed += AddProfessor_Closed;

            updateProfesor.Show();
        }

        private void DeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            if (professor == null)
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite profesora kojeg zelite da obrisete sa katedre.");
                }
                else
                {
                    MessageBox.Show("Please select professor that you want to delete from department.");
                }

            }
            else
            {
                string message = "";
                string title = "";
                if (GlobalData.SharedString == "sr-RS")
                {
                    message = "Da li ste sigurni da zelite da obrisete profesora?";
                    title = "Brisanje profesora sa katedre";
                }
                else
                {
                    message = "Are you sure that you want to delete this professr?";
                    title = "Deleting professor from department";
                }

                MessageBoxResult result =
                 MessageBox.Show(message, title,
       MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {

                    professorDepartmentController.Delete(department.departmentId);
                    Update();

                }

                else
                { }
            }
        }


    }
}
