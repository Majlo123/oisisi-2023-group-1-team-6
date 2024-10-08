﻿using CLI.Controller;
using CLI.Model;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using static GUI.MainWindow;
using Index = StudentskaSluzba.Model.Index;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged, IDataErrorInfo
    {

        private GradeController controller;
        private SubjectController subjectController;
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
                    OnPropertyChanged("AbbreviationOfMajor");
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
                    OnPropertyChanged("MarkOfMajor");
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
                    OnPropertyChanged("YearOfEnrollment");
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
                    OnPropertyChanged("IdIndex");
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
                    OnPropertyChanged("Name");
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
                    OnPropertyChanged("Surname");
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
                OnPropertyChanged("YearOfStudy");
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
                    OnPropertyChanged("Status");
                }
            }
        }

        public double average;

        public double Average
        {
            get
            {
                controller = new GradeController();
                double suma = 0;
                int i = 0;
                foreach (Grade g in controller.GetAllGradesByStudent(id))
                {
                    suma += g.grades;
                    i++;
                }
                if (suma == 0)
                {
                    return 0;
                }
                return suma / i;
            }
            set
            {
                if (value != average)
                {
                    average = value;
                    OnPropertyChanged("Average");
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
                    OnPropertyChanged("Id");
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
                    OnPropertyChanged("Date");
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
                    OnPropertyChanged("Phonenumber");
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

        public int idAddress;

        public int IdAddress
        {
            get
            {
                return idAddress;
            }
            set
            {
                if (value != idAddress)
                {
                    idAddress = value;
                    OnPropertyChanged("IdAddress");
                }
            }
        }

        public String printIndex;

        public String PrintIndex
        {
            get
            {
                return abbreviationOfMajor + markOfMajor + "/" + yearOfEnrollment;
            }
            set
            {
                if (value != printIndex)
                {
                    printIndex = value;
                    OnPropertyChanged("PrintIndex");
                }
            }
        }

        public int espbpoints;

        public int Espbpoints
        {
            get
            {
                int suma = 0;
                List<Grade> grade = controller.GetAllGradesByStudent(id);
                subjectController = new SubjectController();
                if (grade == null)
                    return 0;
                foreach (Grade g in grade)
                {
                    Subject subject = subjectController.getSubjectById(g.subjectId);
                    suma += subject.ESPBPoints;
                }
                return suma;
            }
            set
            {
                if (value != espbpoints)
                {
                    espbpoints = value;
                    OnPropertyChanged("Espbpoints");
                }
            }
        }



        private Regex _nameRegex = new Regex("[A-Za-zČčĆćŠšĐđŽž]+");
        private Regex _addressRegex = new Regex("[A-Za-zČčĆćŠšĐđŽž]+");
        private Regex _surnameRegex = new Regex("[A-Za-zČčĆćŠšĐđŽž]+");
        private Regex _idRegex = new Regex("[0-9]+");
        private Regex _PhoneNumberRegex = new Regex("[+][0-9]{7,15}");
        private Regex _StatusRegex = new Regex("[BS]");
        private Regex _dateRegex = new Regex("[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}");
        private Regex _emailRegex = new Regex("[a-zA-Z]+[.][a-zA-Z]+[.][0-9]+[.][0-9]+[@][a-zA-Z]+[.][a-zA-Z]+");
        private Regex _naturalNumberRegex = new Regex("[1-9][0-9]*[A-Za-z]*");
        private Regex _abbreviationRegex = new Regex("[A-Z]{2}");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {

                    if (string.IsNullOrEmpty(Name))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Ime ne sme biti prazno";
                        }
                        else
                        {
                            return "Name is required";
                        }
                    }

                    Match match = _nameRegex.Match(Name);
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
                else if (columnName == "Surname")
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
                    Match match = _surnameRegex.Match(Surname);
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
                else if (columnName == "Date")
                {
                    if (string.IsNullOrEmpty(Date.ToString()))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Datum ne sme biti prazan";
                        }
                        else
                        {
                            return "Date is required";
                        }
                    }


                    Match match = _dateRegex.Match(Date.ToString());
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
                else if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Ulica ne sme biti prazna";
                        }
                        else
                        {
                            return "Street is required";
                        }
                    }

                    Match match = _addressRegex.Match(Street);
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
                else if (columnName == "Number")
                {
                    if (Number == 0)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Broj ulice ne sme biti prazan";
                        }
                        else
                        {
                            return "Number is required";
                        }
                    }


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

                    Match match = _addressRegex.Match(City);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Format nije dobar. Pokusaj ponovo";
                        }
                        else
                        {
                            return "Format not good. Try again. ";
                        }
                    }

                }
                else if (columnName == "State")
                {
                    if (string.IsNullOrEmpty(State))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Drzava ne sme biti prazno";
                        }
                        else
                        {
                            return "State is required";
                        }
                    }

                    Match match = _addressRegex.Match(State);
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
                else if (columnName == "Phonenumber")
                {
                    if (string.IsNullOrEmpty(Phonenumber))
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

                    Match match = _PhoneNumberRegex.Match(Phonenumber);
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
                            return "Email ne sme biti prazno";
                        }
                        else
                        {
                            return "Email is required";
                        }
                    }


                    Match match = _emailRegex.Match(Email);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Email=prezime.smer.god@domen.eksentzijadomena";
                        }
                        else
                        {
                            return "Email=surname.abb.year@domain.domainextension ";
                        }
                    }

                }
                else if (columnName == "Id")
                {

                    Match match = _idRegex.Match(Id.ToString());
                    if (!match.Success)
                        return "Id must be in format of nine numbers ";
                }
                else if (columnName == "Average")
                {
                    Match match = _naturalNumberRegex.Match(Average.ToString());
                    if (!match.Success)
                        return "Average must be in format of number";
                }
                else if (columnName == "YearOfStudy")
                {
                    if (YearOfStudy == 0)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Godina studija ne sme biti prazno";
                        }
                        else
                        {
                            return "Year of study is required";
                        }
                    }
                    Match match = _naturalNumberRegex.Match(YearOfStudy.ToString());
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Godina mora biti u formatu broja";
                        }
                        else
                        {
                            return "Year must be in format off number";
                        }
                    }
                }
                else if (columnName == "Status")
                {
                    if (string.IsNullOrEmpty(Status))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Status ne sme biti prazan";
                        }
                        else
                        {
                            return "Status is required";
                        }
                    }

                    Match match = _StatusRegex.Match(Status);
                    if (!match.Success)
                        return "Status must be 'B' or 'S'";
                }
                else if (columnName == "AbbreviationOfMajor")
                {
                    if (string.IsNullOrEmpty(AbbreviationOfMajor))
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Oznaka smera je obavezna";
                        }
                        else
                        {
                            return "Abbreviation is required";
                        }
                    }


                    Match match = _abbreviationRegex.Match(AbbreviationOfMajor);
                    if (!match.Success)
                    {
                        if (GlobalData.SharedString == "sr-RS")
                        {
                            return "Smer je u formatu 2 velika slova";
                        }
                        else
                        {
                            return "Abbreviation consist 2 big letters";
                        }
                    }
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = {"Name", "Surname","Date", "Street", "Number", "City", "State",
                                                           "PhoneNumber", "Email", "Id", "Average", "Email", "YearOfStudy", "Status",
                                                          "AbbreviationOfMajor", "MarkOfMajor", "YearOfEnrollment", "IdIndex"};

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
            Index index = new Index(idIndex, abbreviationOfMajor, markOfMajor, yearOfEnrollment);
            Student student = new Student(surname, name, id, date, adresa, phonenumber, email, index, yearOfStudy, status, average);
            return student;
        }


        public StudentDTO(Student s, Address a, Index i)
        {
            a = s.Address;
            i = s.Index;
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
            printIndex = i.AbbreviationOfMajor + i.MarkOfMajor + "/" + i.YearOfEnrollment;
        }

        public Address ToAddress()
        {
            return new Address(idAddress, street, number, city, state);

        }

        public Index ToIndex()
        {
            return new Index(idIndex, abbreviationOfMajor, markOfMajor, yearOfEnrollment);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
