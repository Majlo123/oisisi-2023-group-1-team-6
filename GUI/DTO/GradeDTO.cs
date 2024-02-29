using CLI.Controller;
using StudentskaSluzba.Model;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GUI.DTO
{


    public class GradeDTO
    {

        private SubjectController controller;
        public SubjectDTO Subject_1 { get; set; }
        public int subjectId;

        public int SubjectId
        {
            get
            {
                return subjectId;
            }
            set
            {
                if (subjectId != value)
                {
                    subjectId = value;
                    OnPropertyChanged("SubjectId");
                }
            }
        }

        public string subjectName;

        public string SubjectName
        {
            get
            {
                return subjectName;
            }
            set
            {
                if (subjectName != value)
                {
                    subjectName = value;
                    OnPropertyChanged("SubjectName");
                }
            }
        }
        public int studentId;

        public int StudentId
        {
            get
            {
                return studentId;
            }
            set
            {
                if (studentId != value)
                {
                    studentId = value;
                    OnPropertyChanged("StudentId");
                }
            }
        }

        public int ESPBpoints;

        public int ESPBPoints
        {
            get
            {
                return ESPBpoints;
            }
            set
            {
                if (ESPBpoints != value)
                {
                    ESPBpoints = value;
                    OnPropertyChanged("ESPBPoints");
                }
            }
        }

        private double grades;
        public double Grade
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
        private readonly string[] _validatedProperties = { "Grade", "Date" };
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

            return new Grade(studentId, subjectId, date, grades);
        }
        public GradeDTO()
        {
        }
        public GradeDTO(Grade grade)
        {
            controller = new SubjectController();
            studentId = grade.studentId;
            subjectId = grade.subjectId;
            subjectName = controller.getSubjectById(grade.subjectId).subjectName;
            ESPBPoints = controller.getSubjectById(grade.subjectId).ESPBPoints;
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

