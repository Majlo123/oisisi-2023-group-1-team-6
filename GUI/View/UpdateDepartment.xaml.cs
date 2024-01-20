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
    /// Interaction logic for UpdateDepartment.xaml
    /// </summary>
    public partial class UpdateDepartment : Window
    {

        public string name;
        public string surname;
        public string email;
        public string title;
        public string professorId;
        public ProfessorDTO professor { get; set; }
        public DepartmentDTO department { get; set; }

        private ProfessorController professorController;
        private ProfessorSubjectController professorSubjectController;
        private AddressController addressController;

        private DepartmentController departmentController;

        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public UpdateDepartment(DepartmentController dc, DepartmentDTO department)
        {
            InitializeComponent();
            this.department = department;
            professor = new ProfessorDTO();
            this.name = professor.Name;
            this.surname = professor.Surname;
            this.email = professor.Email;
            this.title = professor.Title;
            this.department = department;
            professorController = new ProfessorController();
            professorSubjectController = new ProfessorSubjectController();
            this.departmentController = dc;
            addressController = new AddressController();
            Professors = new ObservableCollection<ProfessorDTO>();
            DataContext = this;
            Update();
        }

        public void Update()
        {
             Professors.Clear();
             foreach (Address address in addressController.GetAllAddress())
                 foreach (Professor professor in departmentController.getProfessorsByDepartmentId(department.departmentId))
                 {
                     if (professor.Address.id == address.id)
                            Professors.Add(new ProfessorDTO(professor, address));
                 }


        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Professors.Clear();
            foreach (Professor professor in departmentController.getProfessorsByDepartmentId(department.departmentId))
                Professors.Add(new ProfessorDTO(professor));
        }

        private void AddProfessor_Click(object sender, RoutedEventArgs e)
        {
            AddProfessorToDepartment updateProfesor = new AddProfessorToDepartment(professorController,departmentController,department,this);
            updateProfesor.Closed += AddProfessor_Closed;

            updateProfesor.Show();
        }

        private void AddProfessor_Closed(object sender, EventArgs e)
        {
            Update();
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
            AddProfessorToDepartment updateProfesor = new AddProfessorToDepartment(professorController, departmentController, department, this);

            updateProfesor.AddBoss_Click(sender, e);
            updateProfesor.Closed += AddProfessor_Closed;

            updateProfesor.Show();
        }

        private void DeleteProfessor_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
