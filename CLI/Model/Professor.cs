using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;

    public  class Professor : ISerializable
    {
    public string Surname { get; set; }

    public string Name { get; set; }

    public DateOnly Date { get; set; }

    public Address Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Id { get; set; }


    public string Title { get; set; }

    public int WorkYear { get; set; }

    public List<Subject> Subjects { get; set; }

    public Professor()
    {
        Subjects = new List<Subject>();
    }

    public Professor(string name, string surname) {
        Name = name;
        Surname = surname;
        
        
    }
    public Professor(string surname, string name, DateOnly date, Address address, string phonenumber, string email, string id, string title, int workyear)
    {
        Surname = surname;
        Name = name;
        Date = date;
        Address = address;
        PhoneNumber = phonenumber;
        Email = email;
        Id = id;
        Title = title;
        WorkYear = workyear;
        Subjects = new List<Subject>();
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id,
            Surname,
            Name,
            Date.ToString(),
            Address.ToString(),
            PhoneNumber,
            Email,
            Title,
            WorkYear.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = values[0];
        Surname = values[1];
        Name = values[2];
        Date = DateOnly.ParseExact(values[3], "M/d/yyyy");
        Address = new Address(int.Parse(values[4]), values[5], int.Parse(values[6]), values[7], values[8]);
        PhoneNumber = values[9];
        Email = values[10];
        Title = values[11];
        WorkYear=int.Parse(values[12]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID:{Id}|");
        sb.Append($"SURNAME:{Surname}|");
        sb.Append($"NAME:{Name}|");
        sb.Append($"DATE OF BIRTH:{Date}|");
        sb.Append($"ADDRESS:{Address.ToString()}|");
        sb.Append($"PHONE NUMBER:{PhoneNumber}|");
        sb.Append($"EMAIL:{Email}|");
        sb.Append($"TITLE:{Title}|");
        sb.Append($"WORK YEAR:{WorkYear.ToString()}|");
        sb.Append("SUBJECTS:");
        sb.AppendJoin("|", Subjects.Select(subject => subject.subjectName));

        return sb.ToString();
    }
}

