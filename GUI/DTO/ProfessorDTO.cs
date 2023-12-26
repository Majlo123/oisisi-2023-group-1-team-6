﻿using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ProfessorDTO:INotifyPropertyChanged,IDataErrorInfo
    {

        //string surname, string name, DateOnly date,
        //string street, int number, string city, string state
        //string phonenumber, string email, int id, string title, int workyear
        private string surname;
        public string Surname
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
        private string name;
        public string Name
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
        private DateOnly date;
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
        private string phone;
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if (value != phone)
                {
                    phone = value;
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
        private string id;
        public string Id
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
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }
        private int workyear;
        public int Workyear
        {
            get
            {
                return workyear;
            }
            set
            {
                if (value != workyear)
                {
                    workyear = value;
                    OnPropertyChanged();
                }
            }
        }
      
        public string Error => null;

        private Regex _NameRegex = new Regex("[A-Za-z0-9-]+");
        private Regex _IdRegex = new Regex("[0-9]{9}");
        private Regex _PhoneNumberRegex = new Regex("[+][0-9]{7,15}");
      
        private Regex _EmailRegex = new Regex("[a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]+");
        private Regex _DateRegex = new Regex("[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}");

        //int subjectid, string subjectname, int yearofstudy, string Semester,int eSPBPoints
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Surname")
                {
                    if (string.IsNullOrEmpty(Surname))
                        return "Surname is required";

                    Match match = _NameRegex.Match(Surname);
                    if (!match.Success)
                        return "Format not good. Try again.";

                }
                else if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                    Match match = _NameRegex.Match(Name);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "Date")
                {
                    
                }
                else if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))
                        return "Street is required";

                    Match match = _NameRegex.Match(Street);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "Number")
                {
                    
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required";

                    Match match = _NameRegex.Match(City);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "State")
                {
                    if (string.IsNullOrEmpty(State))
                        return "State is required";

                    Match match = _NameRegex.Match(State);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "Phone")
                {
                    if (string.IsNullOrEmpty(Phone))
                        return "Phone number is required";

                    Match match = _PhoneNumberRegex.Match(Phone);
                    if (!match.Success)
                        return "Number must begin with + then State area code and then your number ";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Email is required";

                    Match match = _EmailRegex.Match(Email);
                    if (!match.Success)
                        return "Email must be in format name@domain.domainextension ";
                }
                else if (columnName == "Id")
                {
                    if (string.IsNullOrEmpty(Id))
                        return "Id is required";

                    Match match = _IdRegex.Match(Id);
                    if (!match.Success)
                        return "Id must be in format of nine numbers ";
                }
                else if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                        return "Title is required";

                    Match match = _NameRegex.Match(Title);
                    if (!match.Success)
                        return "Format not good. Try again. ";
                }
                else if (columnName == "Workyear")
                {
                    
                }
               
                return null;
            }
        }

        private readonly string[] _validatedProperties = {"Name", "Surname","Date", "Street", "Number", "City", "State",
                                                           "Phone", "Email", "Id", "Title",
                                                           "Workyear"};

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
        
        public Professor ToProfessor()
        {
            Address address = new Address(street,number,city,state);
            return new Professor(surname,name,date,address,phone,email,id,title,workyear);
        }
       
        public ProfessorDTO()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ProfessorDTO(Professor professor,Address address)
        {
            surname = professor.Surname;
            name = professor.Name;
            date=professor.Date;
            street = address.Street;
            number=address.Number;
            city = address.City;
            state = address.State;
            address = professor.Address;
            phone = professor.PhoneNumber;
            email = professor.Email;
            id = professor.Id;
            title = professor.Title;
            workyear = professor.WorkYear;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
