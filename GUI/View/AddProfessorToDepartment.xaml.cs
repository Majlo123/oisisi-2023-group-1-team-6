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
        public ProfessorController professor_controler;
        public ProfessorDepartmentController professordepartment_controler;
        public DepartmentDTO SelectedDepartment { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        public UpdateDepartment updateDepartment { get; set; }
        public AddProfessorToDepartment(DepartmentDTO Department, ProfessorDTO Professor)
        {
            InitializeComponent();
            professor_controler = new ProfessorController();
            professordepartment_controler = new ProfessorDepartmentController();
            DataContext = this;
            SelectedDepartment = Department;
            SelectedProfessor = Professor;
            Professors = new ObservableCollection<ProfessorDTO>();
            Update();
        }

        private void Update()
        {

            Professors.Clear();

            foreach (Professor subject in professor_controler.GetAllProfessors()) Professors.Add(new ProfessorDTO(subject));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDepartment != null)
            {
                professordepartment_controler.Add(SelectedProfessor.Id, SelectedDepartment.departmentId);

                Close();
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberi predmet koji zelis da dodas.");
                }
                else
                {
                    MessageBox.Show("Chose subject that you want to add.");
                }

            }
        }

        public void AddBoss_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor != null)
            {
                string id_boss = SelectedProfessor.Id;
                SelectedDepartment.professorId = id_boss;
                professor_controler.Update(SelectedProfessor.ToProfessor());


                updateDepartment.department.professorId = id_boss;

                Close();
            }
            else
            {
                if (GlobalData.SharedString == "sr-RS")
                {
                    MessageBox.Show("Izaberite profesora kojeg zelite da dodate.");
                }
                else
                {
                    MessageBox.Show("Chose professor that you want to add.");
                }
            }
        }

    }
}
