﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;
public enum finance { B, S }
public class Student : ISerializable
{
    public string Surname { get; set; }

    public string Name { get; set; }

    public int Id {  get; set; }

    public DateOnly Date { get; set; }

    public Address Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public Index Index { get; set; }

    public int YearOfStudy { get; set; }

    public finance Status;

    public float AvarageGrade { get; set; }

    public List<Subject> PassedSubjects { get; set; }

    public List<Subject> FailedSubjects { get; set; }


    public Student()
    {
        PassedSubjects = new List<Subject>();
        FailedSubjects = new List<Subject>();
    }

    public Student(string surname, string name,int id, DateOnly date, Address address, string phonenumber, string email, Index index, int yearofstudy, finance status, float avaragegrade)
    {
        Surname = surname;
        Name = name;
        Id = id;
        Date = date;
        Address = address;
        PhoneNumber = phonenumber;
        Email = email;
        Index = index;
        YearOfStudy = yearofstudy;
        Status = status;
        AvarageGrade = avaragegrade;
        PassedSubjects = new List<Subject>();
        FailedSubjects = new List<Subject>();
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Surname,
            Name,
            Id.ToString(),
            Date.ToString(),
            Address.ToString(),
            PhoneNumber,
            Email,
            Index.ToString(),
            YearOfStudy.ToString(),
            Status.ToString(),
            AvarageGrade.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Surname = values[0];
        Name = values[1];
        Id = int.Parse(values[2]);
        Date = DateOnly.ParseExact(values[3], "dd-MM-yyyy");
        Address.Street = values[4];
        Address.Number = int.Parse(values[5]);
        Address.City = values[6];
        Address.State = values[7];
        PhoneNumber = values[8];
        Email = values[9];
        Index.AbbreviationOfMajor = values[10];
        Index.MarkOfMajor = int.Parse(values[11]);
        Index.YearOfEnrollment = int.Parse(values[12]);
        YearOfStudy = int.Parse(values[13]);
        Status = (finance)int.Parse(values[14]);
        AvarageGrade = int.Parse(values[15]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"SURNAME: {Surname}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"DATE OF BIRTH: {Date.ToString()}, ");
        sb.Append($"ADDRESS: {Address.ToString()}, ");
        sb.Append($"PHONE NUMBER: {PhoneNumber.ToString()}, ");
        sb.Append($"EMAIL: {Email}, ");
        sb.Append($"INDEX NUMBER: {Index.ToString()}, ");
        sb.Append($"YEAR OF STUDY: {YearOfStudy.ToString()}, ");
        sb.Append($"STUDDY YEAR STATUS: {Status.ToString()}, ");
        sb.Append($"AVERAGE GRADE: {AvarageGrade.ToString()}, ");
        sb.Append("SUBJECTS PASSED:");
        sb.AppendJoin(", ", PassedSubjects.Select(subject => subject.subjectName));
        sb.Append($"SUBJECTS FAILED:");
        sb.AppendJoin(", ", FailedSubjects.Select(subject => subject.subjectName));//ovo se primenjuje posle pravljenja klase subject


        return sb.ToString();
    }

    
}
