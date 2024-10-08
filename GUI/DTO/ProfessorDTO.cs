﻿using CLI.Controller;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using static GUI.MainWindow;

namespace GUI.DTO
{
    public class ProfessorDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        public AddressController addressController { get; set; }

        //string surname, string name, DateOnly date,
        //string street, int number, string city, string state
        //string phonenumber, string email, int id, string title, int workyear

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
                    OnPropertyChanged("Surname");
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
                    OnPropertyChanged("Name");
                }
            }
        }

        public int departmentId;

        public int DepartmentId
        {
            get
            {
                return departmentId;
            }
            set
            {
                if (value != departmentId)
                {
                    departmentId = value;
                    OnPropertyChanged("DepartmentId");
                }
            }
        }

        private int addresid;
        public int AddressId
        {
            get
            {
                return addresid;
            }
            set
            {
                if (value != addresid)
                {
                    addresid = value;
                    OnPropertyChanged("AddressId");
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
                    OnPropertyChanged("Street");
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
                    OnPropertyChanged("Number");
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
                    OnPropertyChanged("City");
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
                    OnPropertyChanged("State");
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
                    OnPropertyChanged("Phone");
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
                    OnPropertyChanged("Email");
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
                    OnPropertyChanged("Id");
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
                    OnPropertyChanged("Title");
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
                    OnPropertyChanged("Workyear");
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
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Prezime ne sme biti prazno";
                        }
                        else
                        {
                            return "Surname is required";
                        }
                    }


                    Match match = _NameRegex.Match(Surname);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo";
                        }
                        else
                        {
                            return "Format not good. Try again.";

                        }
                    }

                }
                else if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Ime ne sme biti prazno";
                        }
                        else
                        {
                            return "Name is required";

                        }

                    Match match = _NameRegex.Match(Name);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo";
                        }
                        else
                        {
                            return "Format not good. Try again.";

                        }
                    }
                }
                else if (columnName == "Date")
                {

                }
                else if (columnName == "AddressId")
                {

                }
                else if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Ime ulice ne sme biti prazno";
                        }
                        else
                        {
                            return "Street is required";

                        }
                    }

                    Match match = _NameRegex.Match(Street);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo";
                        }
                        else
                        {
                            return "Format not good. Try again.";

                        }
                    }
                }
                else if (columnName == "Number")
                {

                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Grad ne sme biti prazan";
                        }
                        else
                        {
                            return "City is required";

                        }
                    }


                    Match match = _NameRegex.Match(City);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo";
                        }
                        else
                        {
                            return "Format not good. Try again.";

                        }
                    }
                }
                else if (columnName == "State")
                {
                    if (string.IsNullOrEmpty(State))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Drazava ne sme biti prazna";
                        }
                        else
                        {
                            return "State is required";

                        }
                    }

                    Match match = _NameRegex.Match(State);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo";
                        }
                        else
                        {
                            return "Format not good. Try again.";

                        }
                    }
                }
                else if (columnName == "Phone")
                {
                    if (string.IsNullOrEmpty(Phone))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Broj telefona ne sme biti prazan";
                        }
                        else
                        {
                            return "Phone is required";

                        }
                    }

                    Match match = _PhoneNumberRegex.Match(Phone);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Broj= '+' onda pozivni broj,onda broj telefona";
                        }
                        else
                        {
                            return "Number= '+' then State area code,then your number ";
                        }
                    }
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Email ne sme biti prazan";
                        }
                        else
                        {
                            return "Email is required";

                        }
                    }

                    Match match = _EmailRegex.Match(Email);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Email=ime.prezime@domen.eksentzijadomena";
                        }
                        else
                        {
                            return "Email=name.surname@domain.domainextension ";
                        }
                    }
                }
                else if (columnName == "Id")
                {
                    if (string.IsNullOrEmpty(Id))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Broj licne karte ne sme biti prazan";
                        }
                        else
                        {
                            return "Id is required";

                        }
                    }

                    Match match = _IdRegex.Match(Id);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Broj licne mora imate 9 brojeva";
                        }
                        else
                        {
                            return "Id must be in format of 9 numbers";

                        }
                    }
                }
                else if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Zvanje ne sme biti prazno";
                        }
                        else
                        {
                            return "Title is required";

                        }
                    }

                    Match match = _NameRegex.Match(Title);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo";
                        }
                        else
                        {
                            return "Format not good. Try again.";

                        }
                    }
                }
                else if (columnName == "Workyear")
                {

                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = {"Name", "Surname","Date","AddressId","Street", "Number", "City", "State",
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
            Address address = new Address(addresid, street, number, city, state);
            return new Professor(surname, name, date, address, phone, email, id, title, workyear);
        }


        public Address ToAddress()
        {
            return new Address(addresid, street, number, city, state);

        }
        public ProfessorDTO()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ProfessorDTO(Professor professor, Address address)
        {
            surname = professor.Surname;
            name = professor.Name;
            date = professor.Date;
            addresid = address.id;
            street = address.Street;
            number = address.Number;
            city = address.City;
            state = address.State;
            address = professor.Address;
            phone = professor.PhoneNumber;
            email = professor.Email;
            id = professor.Id;
            title = professor.Title;
            workyear = professor.WorkYear;

        }
        public static List<ProfessorDTO> ConvertList(List<Professor> professors)
        {
            return professors.Select(professor => new ProfessorDTO(professor)).ToList();
        }
        public ProfessorDTO(Professor professor)
        {
            surname = professor.Surname;
            name = professor.Name;
            id = professor.Id;
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
