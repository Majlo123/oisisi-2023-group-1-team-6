using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ShowStudentsWindow.xaml
    /// </summary>
    public partial class ShowStudentsWindow : Window
    {
        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;

                OnPropertyChanged("SearchText");
            }
        }

        public StudentDTO selectedStudent { get; set; }

        public ObservableCollection<StudentDTO> Students { get; set; }
        private StudentController studentController;
        private AddressController addressController;
        private IndexController indexController;
        public ShowStudentsWindow(List<StudentDTO> students)
        {
            InitializeComponent();
            Students = new ObservableCollection<StudentDTO>(students);
            DataContext = this;
            studentController = new StudentController();
            addressController = new AddressController();
            indexController = new IndexController();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string writentext = SearchText.ToLower();
                writentext = writentext.Replace(" ", String.Empty);
                string[] query = writentext.Split(',');
                ObservableCollection<StudentDTO> studentTemp = new ObservableCollection<StudentDTO>();

                if (query.Length == 1)
                {
                    foreach (StudentDTO st in Students)
                    {
                        if (st.surname.ToLower().Contains(query[0]))
                        {
                            studentTemp.Add(st);
                        }
                    }
                }
                else if (query.Length == 2)
                {
                    foreach (StudentDTO st in Students)
                    {
                        if (st.surname.ToLower().Contains(query[0]))
                        {
                            if (st.name.ToLower().Contains(query[1]))
                            {
                                studentTemp.Add(st);
                            }
                        }
                    }
                }
                else if (query.Length == 3)
                {
                    foreach (StudentDTO st in Students)
                    {
                        if (st.printIndex.ToLower().Contains(query[0]))
                        {
                            if (st.name.ToLower().Contains(query[1]))
                            {
                                if (st.surname.ToLower().Contains(query[2]))
                                    studentTemp.Add(st);
                            }
                        }
                    }
                }
                Students = studentTemp;
                PassedFailedDataGrid.ItemsSource = Students;

            }
            else
            {
                Students.Clear();
                var addresses = addressController.GetAllAddress();
                var students = studentController.GetAllStudents();
                var indexes = indexController.GetAllIndexes();

                foreach (Student student in students)
                {
                    Address studentAddress = addressController.getAddressById(student.Address.id);
                    StudentskaSluzba.Model.Index studentIndex = indexController.getIndexById(student.Index.IdIndex);
                    Students.Add(new StudentDTO(student, studentAddress, studentIndex));
                }



                PassedFailedDataGrid.ItemsSource = Students;

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
