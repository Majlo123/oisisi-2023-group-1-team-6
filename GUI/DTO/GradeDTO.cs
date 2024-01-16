using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GUI.DTO
{
    

    public class GradeDTO
    {
       
        public SubjectDTO Subject_1 { get; set; }
        public int Subject_Id;
        public int Student_Id;

        private string grades;
        public string Grade
        {
            get
            {
                return grades;
            }
            set
            {
                if (grades != value)
                {
                    grades = value;
                    OnPropertyChanged("Grade");
                }
            }
        }
        public DateOnly date;

        public DateOnly Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }
        public string Error => null;
        private Regex _NameRegex = new Regex("[A-Za-z0-9-]+");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Grade")
                {


                }
                else if (columnName == "Date")
                {
                    
                }
                


                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Grade", "Date"};
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        public Grade ToGrade()
        {

            return new Grade(date,grades);
        }
        public GradeDTO()
        {
        }
        public GradeDTO(Grade grade)
        {
            grades = grade.grades;
            date = grade.date;
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

