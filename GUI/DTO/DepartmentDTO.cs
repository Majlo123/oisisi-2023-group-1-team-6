using StudentskaSluzba.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using static GUI.MainWindow;

namespace GUI.DTO
{
    public class DepartmentDTO : INotifyPropertyChanged
    {
        public int departmentId;

        public int DepartmentId
        {
            get
            {
                return departmentId;
            }
            set
            {
                if (departmentId != value)
                {
                    departmentId = value;
                    OnPropertyChanged("DepartmentId");
                }
            }
        }

        public string code;

        public string Code
        {
            get
            {
                return "k" + departmentId;
            }
            set
            {
                if (code != value)
                {
                    code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        public string departmentName;

        public string DepartmentName
        {
            get
            {
                return departmentName;
            }
            set
            {
                if (departmentName != value)
                {
                    departmentName = value;
                    OnPropertyChanged("DepartmentName");
                }
            }
        }

        public string professorId;

        public string ProfessorId
        {
            get
            {
                return professorId;
            }
            set
            {
                if (professorId != value)
                {
                    professorId = value;
                    OnPropertyChanged("ProfessorId");
                }
            }
        }

        public List<ProfessorDTO> professors;

        public List<ProfessorDTO> Professors
        {
            get
            {
                return professors;
            }
            set
            {
                if (value != professors)
                {
                    professors = value;
                    OnPropertyChanged("Professors");
                }
            }

        }

        private Regex _nameRegex = new Regex("[A-Z][a-z0-9]+");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "DepartmentName")
                {
                    if (string.IsNullOrEmpty(DepartmentName))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Ime departmana ne sme biti prazno";
                        }
                        else
                        {
                            return "Name is required";

                        }
                    }

                    Match match = _nameRegex.Match(DepartmentName);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo.";
                        }
                        else
                        {
                            return "Format isn't good. Try again.";

                        }
                    }

                }

                return null;
            }
        }

        private readonly string _validatedProperties = "DepartmentName";

        public bool IsValid
        {
            get { return string.IsNullOrEmpty(_validatedProperties); }
        }

        public DepartmentDTO()
        {

        }

        public DepartmentDTO(Department d)
        {
            departmentId = d.DepartmentID;
            departmentName = d.DepartmentName;
            ProfessorId = d.DepartmentBoss;
            professors = new List<ProfessorDTO>();
        }

        public Department toDepartment()
        {
            return new Department(departmentId, departmentName, professorId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
