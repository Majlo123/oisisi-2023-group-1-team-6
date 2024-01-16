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
    /// Interaction logic for AddProfessorToSubject.xaml
    /// </summary>
    public partial class AddProfessorToSubject : Window
    {
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        private ProfessorController professorController;
        private AddressController addressController;
        private SubjectController subjectController;

        public SubjectDTO SelectedSubject { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        public AddProfessorToSubject(ProfessorController ps, SubjectController sc, SubjectDTO selectedSubject)
        {
            InitializeComponent();
            professorController = ps;
            addressController = new AddressController();
            subjectController = sc;
            DataContext = this;
            SelectedSubject = selectedSubject;
            Professors = new ObservableCollection<ProfessorDTO>();
            Update();
        }
        public void Update()
        {


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
            string id_prof = SelectedProfessor.Id;
            SelectedSubject.professorId = id_prof;
            subjectController.Update(SelectedSubject.ToSubject());


            Update();

            Close();
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
