using CLI.Controller;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for UpdateDepartment.xaml
    /// </summary>
    public partial class UpdateDepartment : Window
    {
        public ProfessorDTO Professor { get; set; }
        public DepartmentDTO Department { get; set; }

        private AddressController addressController;
        private ProfessorController professorController;
        private DepartmentController departmentController;
        public ObservableCollection<ProfessorDTO> professors { get; set; }
        public UpdateDepartment(DepartmentController dc, DepartmentDTO selectedDepartment)
        {
            InitializeComponent();

            professorController = new ProfessorController();
            addressController = new AddressController();
            departmentController = dc;
            Department = selectedDepartment;
        }

        private void Add_Click(object  sender, RoutedEventArgs e)
        {
            
        }


        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}
