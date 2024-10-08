﻿using CLI.Model;
using StudentskaSluzba.Serialization;
using System.Text;
namespace StudentskaSluzba.Model;
public class Student : ISerializable
{
    public string Surname { get; set; }

    public string Name { get; set; }

    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public Address Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public Index Index { get; set; }

    public int YearOfStudy { get; set; }

    public string Status { get; set; }

    public double AvarageGrade { get; set; }

    public List<Subject> PassedSubjects { get; set; }

    public List<Subject> FailedSubjects { get; set; }


    public Student(string surname, string name)
    {
        Surname = surname;
        Name = name;

    }
    public Student()
    {
        PassedSubjects = new List<Subject>();
        FailedSubjects = new List<Subject>();
    }

    public Student(string surname, string name, int id, DateOnly date, Address address, string phonenumber, string email, Index index, int yearofstudy, string status, double avaragegrade)
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
            Id.ToString(),
            Surname,
            Name,
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
        Id = int.Parse(values[0]);
        Surname = values[1];
        Name = values[2];
        Date = DateOnly.ParseExact(values[3], "M/d/yyyy");
        Address = new Address(int.Parse(values[4]), values[5], int.Parse(values[6]), values[7], values[8]);
        PhoneNumber = values[9];
        Email = values[10];
        Index = new Index(int.Parse(values[11]), values[12], int.Parse(values[13]), int.Parse(values[14]));
        YearOfStudy = int.Parse(values[15]);
        Status = values[16];
        AvarageGrade = float.Parse(values[17]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"SURNAME: {Surname}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"DATE OF BIRTH: {Date.ToString()}, ");
        sb.Append($"ADDRESS: {Address.ToString()}, ");
        sb.Append($"PHONE NUMBER: {PhoneNumber.ToString()}, ");
        sb.Append($"EMAIL: {Email}, ");
        sb.Append($"INDEX: {Index.ToString()}, ");
        sb.Append($"YEAR OF STUDY: {YearOfStudy.ToString()}, ");
        sb.Append($"STUDDY YEAR STATUS: {Status}, ");
        sb.Append($"AVERAGE GRADE: {AvarageGrade.ToString()}, ");
        sb.Append("SUBJECTS PASSED:");
        sb.AppendJoin(", ", PassedSubjects.Select(subject => subject.subjectName));
        sb.Append($"SUBJECTS FAILED:");
        sb.AppendJoin(", ", FailedSubjects.Select(subject => subject.subjectName));


        return sb.ToString();
    }


}
