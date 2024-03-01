using CLI.Model;
using StudentskaSluzba.Model;
using System.ComponentModel;
using System.Text.RegularExpressions;
using static GUI.MainWindow;

namespace GUI.DTO
{
    public class SubjectDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        public Subject subject;
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
        public string professor;

        public string Professor
        {
            get
            {
                return professor;
            }
            set
            {
                if (professor != value)
                {
                    professor = value;
                    OnPropertyChanged("Professor");
                }
            }
        }
        public int yearOfStudy;

        public int YearOfStudy
        {
            get
            {
                return yearOfStudy;
            }
            set
            {
                if (yearOfStudy != value)
                {
                    yearOfStudy = value;
                    OnPropertyChanged("YearOfStudy");
                }
            }
        }
        public string semester;

        public string Semester
        {
            get
            {
                return semester;
            }
            set
            {
                if (semester != value)
                {
                    semester = value;
                    OnPropertyChanged("Semester");
                }
            }
        }
        public int espbPoints;

        public int EspbPoints
        {
            get
            {
                return espbPoints;
            }
            set
            {
                if (espbPoints != value)
                {
                    espbPoints = value;
                    OnPropertyChanged("EspbPoints");
                }
            }
        }
        public string Error => null;
        private Regex _NameRegex = new Regex("[A-Za-z0-9-]+");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "SubjectId")
                {


                }
                else if (columnName == "SubjectName")
                {
                    if (string.IsNullOrEmpty(SubjectName))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Ime predmeta ne sme biti prazno";
                        }
                        else
                        {
                            return "Subject name is required";
                        }
                    }


                    Match match = _NameRegex.Match(SubjectName);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo. ";
                        }
                        else
                        {
                            return "Format not good. Try again. ";
                        }
                    }

                }
                else if (columnName == "YearOfStudy")
                {

                }
                else if (columnName == "Semester")
                {
                    if (string.IsNullOrEmpty(Semester))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Semestar ne smi biti prazan";
                        }
                        else
                        {
                            return "Semestar is required";
                        }
                    }

                }
                else if (columnName == "Professor")
                {

                }
                else if (columnName == "EspbPoints")
                {

                }


                return null;
            }
        }
        private readonly string[] _validatedProperties = { "SubjectId", "SubjectName", "YearOfStudy", "Semester", "Professor", "EspbPoints" };
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
        public Subject ToSubject()
        {

            return new Subject(subjectId, subjectName, yearOfStudy, semester, professor, espbPoints);
        }
        public SubjectDTO()
        {
        }
        public SubjectDTO(Subject subject)
        {
            subjectId = subject.subjectId;
            subjectName = subject.subjectName;
            yearOfStudy = subject.yearOfStudy;
            semester = subject.semester;
            professor = subject.professor_Id;
            espbPoints = subject.ESPBPoints;
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
