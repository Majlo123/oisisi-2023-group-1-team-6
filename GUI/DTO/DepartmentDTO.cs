using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                if(departmentName != value)
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
                if(professorId != value)
                {
                    professorId = value;
                    OnPropertyChanged("ProfessorId");
                }
            }
        }

        private Regex _nameRegex = new Regex("[A-Z][a-z0-9]+");

        public string this[string columnName]
        {
            get
            {
                if(columnName == "DepartmentName")
                {
                    if (string.IsNullOrEmpty(DepartmentName))
                        return "Name is required";

                    Match match = _nameRegex.Match(DepartmentName);
                    if (!match.Success)
                        return "Format isn't good. Try again.";
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
        }
        
        public Department toDepartment()
        {
            return new Department(departmentId, departmentName,professorId);
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
