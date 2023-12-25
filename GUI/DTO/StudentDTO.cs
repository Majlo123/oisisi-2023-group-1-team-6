using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Index = StudentskaSluzba.Model.Index;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        public String abbreviationOfMajor;

        public String AbbreviationOfMajor
        {
            get
            {
                return abbreviationOfMajor;
            }
            set
            {
                if (abbreviationOfMajor != value)
                {
                    abbreviationOfMajor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int markOfMajor;

        public int MarkOfMajor
        {
            get
            {
                return markOfMajor;
            }
            set
            {
                if (markOfMajor != value)
                {
                    markOfMajor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int yearOfEnrollment;

        public int YearOfEnrollment
        {
            get
            {
                return yearOfEnrollment;
            }
            set
            {
                if (yearOfEnrollment != value)
                {
                    yearOfEnrollment = value;
                    OnPropertyChanged();
                }
            }
        }

        public int idIndex;

        public int IdIndex
        {
            get
            {
                return idIndex;
            }
            set
            {
                if (idIndex != value)
                {
                    idIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public String name;
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public String surname;

        public String Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged();
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
                yearOfStudy = value;
                OnPropertyChanged();
            }
        }

        public String status;

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                if (!value.Equals(status))
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        public float average;

        public float Average
        {
            get
            {
                return average;
            }
            set
            {
                if (value != average)
                {
                    average = value;
                    OnPropertyChanged();
                }
            }
        }

        public int id;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateOnly date;

        public DateOnly Date
        {
            get
            {
                return date;
            }

            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged();
                }
            }
        }

        private string street;
        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                if (value != street)
                {
                    street = value;
                    OnPropertyChanged();
                }
            }
        }
        private int number;
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value != number)
                {
                    number = value;
                    OnPropertyChanged();
                }
            }
        }
        private string city;
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }
        private string state;
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                if (value != state)
                {
                    state = value;
                    OnPropertyChanged();
                }
            }
        }
        private string phonenumber;
        public string Phonenumber
        {
            get
            {
                return phonenumber;
            }
            set
            {
                if (value != phonenumber)
                {
                    phonenumber = value;
                    OnPropertyChanged();
                }
            }
        }
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        public int idAddress;
        
        public int IdAddress
        {
            get
            {
                return idAddress;
            }
            set
            {
                if(value != idAddress)
                {
                    idAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        private Regex _nameRegex = new Regex("[A-Z][a-z0-9]+");
        private Regex _addressRegex = new Regex("[A-Z][a-z]*");
        private Regex _surnameRegex = new Regex("[A-Z][a-z0-9]+");
        private Regex _idRegex = new Regex("[0-9]+");
        private Regex _phoneNumberRegex = new Regex("06[0-9]{7,8}");
        private Regex _dateRegex = new Regex("[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}");
        private Regex _emailRegex = new Regex("[a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]+");
        private Regex _naturalNumberRegex = new Regex("[1-9][0-9]*[A-Za-z]*");
        private Regex _avgGradeRegex = new Regex("(10(\\.0)?|[1-9](\\.\\[0-9]*)?)");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                    Match match = _nameRegex.Match(Name);
                    if (!match.Success)
                        return "Format isn't good. Try again.";
                }
                else if (columnName == "Surname")
                {
                    if (string.IsNullOrEmpty(Surname))
                        return "Surname is required";

                    Match match = _surnameRegex.Match(Surname);
                    if (!match.Success)
                        return "Format isn't good. Try again.";
                }
                else if (columnName == "Date")
                {
                    if (string.IsNullOrEmpty(Date.ToString())) 
                        return "Date is required";

                    Match match = _dateRegex.Match(Date.ToString());
                    if (!match.Success)
                        return "Format isn't good. Try again.";
                }
                else if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))
                        return "Street is required";

                    Match match = _addressRegex.Match(Street);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "Number")
                {
                    if (Number == 0)
                        return "Street is required";

                    Match match = _idRegex.Match(Id.ToString()); 
                    if (!match.Success)
                        return "Id must be in format of nine numbers ";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required";

                    Match match = _addressRegex.Match(City);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "State")
                {
                    if (string.IsNullOrEmpty(State))
                        return "State is required";

                    Match match = _addressRegex.Match(State);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "Phone number")
                {
                    if (string.IsNullOrEmpty(Phonenumber))
                        return "Phone number is required";

                    Match match = _phoneNumberRegex.Match(Phonenumber);
                    if (!match.Success)
                        return "Number must begin with + then State area code and then your number ";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Email is required";

                    Match match = _emailRegex.Match(Email);
                    if (!match.Success)
                        return "Email must be in format name@domain.domainextension ";
                }
                else if (columnName == "Id")
                {
                    Match match = _idRegex.Match(Id.ToString());
                    if (!match.Success)
                        return "Id must be in format of nine numbers ";
                }
                else if (columnName == "Average grade")
                {
                    Match match = _avgGradeRegex.Match(Average.ToString());
                    if (!match.Success)
                        return "Average must be in format of number.number";
                }
                else if (columnName == "Year of study")
                {
                    Match match = _naturalNumberRegex.Match(YearOfStudy.ToString());
                    if (!match.Success)
                        return "Year of study must be in format of a number";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = {"Name", "Surname","Date", "Id", "Street", "Number", "City", "State",
                                                           "PhoneNumber", "YearOfStudy", "Status", "AvarageGrade", "Email", "Id",
                                                          "AbbreviationOfMajor", "MarkOfMajor", "YearOfEnrollment", "IdIndex", "id"};

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

        public StudentDTO()
        {

        }

        public Student toStudent()
        {
            Address adresa = new Address(idAddress, street, number, city, street);
            Index index = new Index(idIndex,abbreviationOfMajor,markOfMajor,yearOfEnrollment);
            Student student = new Student(surname, name, id, date, adresa, phonenumber, email, index, yearOfStudy, status, average);
            return student;
        }

        public StudentDTO(Student s, Address a, Index i)
        {
            surname = s.Surname;
            name = s.Name;
            id = s.Id;
            date = s.Date;
            yearOfStudy = s.YearOfStudy;
            status = s.Status;
            average = s.AvarageGrade;
            email = s.Email;
            phonenumber = s.PhoneNumber;
            state = a.State;
            city = a.City;
            street = a.Street;
            number = a.Number;
            idIndex = i.IdIndex;
            abbreviationOfMajor = i.AbbreviationOfMajor;
            markOfMajor = i.MarkOfMajor;
            yearOfEnrollment = i.YearOfEnrollment;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
