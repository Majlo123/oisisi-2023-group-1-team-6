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
        private UpdateSubject updateSubject;

        public AddProfessorToSubject(ProfessorController ps, SubjectController sc, SubjectDTO selectedSubject, UpdateSubject updateSubject)
        {
            InitializeComponent();
            professorController = ps;
            addressController = new AddressController();
            subjectController = sc;
            DataContext = this;
            SelectedSubject = selectedSubject;
            this.updateSubject = updateSubject;  
            Professors = new ObservableCollection<ProfessorDTO>();
            Update();
        }
        public void Update()
        {

            subjectController.Update(SelectedSubject.ToSubject());
            
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
                string id_prof = SelectedProfessor.Id;
                SelectedSubject.professor = id_prof;
                subjectController.Update(SelectedSubject.ToSubject());


                updateSubject.professorid = id_prof;
                updateSubject.UpdateProfessorTextbox(id_prof);

                Close();
            }
            else {
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





        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
